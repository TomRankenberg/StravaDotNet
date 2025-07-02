using Data.Models.Strava;
using Statistics.Models;

namespace Statistics.BusinessLogic
{
    public class ActivityStatsConverter
    {
        public static ActivityForStats? ConvertToActivityForStats(DetailedActivity activity, double? avgHeartRate)
        {
            if (activity == null)
            {
                return null;
            }
            return new ActivityForStats
            {
                Id = activity.Id,
                StartDate = activity.StartDate,
                ElapsedTime = activity.ElapsedTime,
                BreakTime = activity.ElapsedTime - activity.MovingTime,
                Distance = activity.Distance,
                ElevationGain = activity.TotalElevationGain,
                Type = activity.Type,
                AverageSpeed = activity.AverageSpeed,
                AverageHeartRate = avgHeartRate
            };
        }

        public static List<ActivityForStats> AddRecentTimeSpent(List<ActivityForStats> activities, int daysToLookBack)
        {
            var sortedActivities = activities
           .Where(a => a.StartDate.HasValue)
           .OrderBy(a => a.StartDate.Value)
           .ToList();

            foreach (var activity in sortedActivities)
            {
                if (!activity.StartDate.HasValue)
                    continue;

                var windowStart = activity.StartDate.Value.AddDays(-daysToLookBack);
                var windowEnd = activity.StartDate.Value;

                // All activities in the 60 days before this activity's StartDate
                var recentActivities = sortedActivities
                    .Where(a =>
                        a.StartDate.HasValue &&
                        a.StartDate.Value > windowStart &&
                        a.StartDate.Value <= windowEnd)
                    .ToList();

                // All activities of the same type in the 60 days before this activity's StartDate
                var recentActivitiesSameType = recentActivities
                    .Where(a => string.Equals(a.Type, activity.Type, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                activity.ActiveRecentTimeAll = recentActivities
                    .Where(a => a.ElapsedTime.HasValue)
                    .Sum(a => (double)a.ElapsedTime.Value);

                activity.ActiveRecentTimeThisType = recentActivitiesSameType
                    .Where(a => a.ElapsedTime.HasValue)
                    .Sum(a => (double)a.ElapsedTime.Value);
            }

            return activities;
        }

        public static List<ActivityForStats> AddPredictedHeartRate(List<ActivityForStats> activities)
        {
            foreach (ActivityForStats activity in activities)
            {
                activity.PredictedHR = CalcPredictedHR(activity);
            }
            
            return activities;
        }

        private static double? CalcPredictedHR(ActivityForStats activity)
        {
            double intercept = 2.895563e+01;
            double recentTimeCoefficient = -3.274816e-05;

            double speedCoefficient = 1.590361e+01;
            double distanceCoefficient = 9.037392e-06;

            if (activity.ActiveRecentTimeAll == null || activity.AverageSpeed == null || activity.Distance == null)
            {
                return null;
            }
            string[] activityTypes = ["Run", "Ride", "Swim"];
            if (!activityTypes.Contains(activity.Type))
            {
                return null;
            }
            switch (activity.Type)
            {
                case "Run":
                    intercept = 1.055645e+02;
                    recentTimeCoefficient = -1.215424e-04;
                    speedCoefficient = 1.822356e+01;
                    distanceCoefficient = 7.652229e-04;
                    break;

                case "Ride":
                    intercept = 5.477113e+01;
                    recentTimeCoefficient = 2.951486e-05;
                    speedCoefficient = 1.191910e+01;
                    distanceCoefficient = 0.0;
                    break;

                case "Swim":
                    intercept = 1.362044e+02;
                    recentTimeCoefficient = 7.808440e-04;
                    speedCoefficient = 0.0;
                    distanceCoefficient = 0.0;
                    break;
            }

            double predictedHR = intercept +
                          speedCoefficient * (activity.AverageSpeed ?? 0) +
                          distanceCoefficient * (activity.Distance ?? 0) +
                          recentTimeCoefficient * (activity.ActiveRecentTimeThisType ?? 0);

            return predictedHR;
        }

        public static double CalcAvgHeartRateError(List<ActivityForStats> activities)
        {
            double averageError = 0.0;

            IEnumerable<ActivityForStats> activitiesWithHRPrediction = activities.Where(a => a.PredictedHR.HasValue && a.PredictedHR > 0 && a.AverageHeartRate.HasValue && a.AverageHeartRate > 0);

            averageError = activitiesWithHRPrediction.Average(a => Math.Abs((double)(a.PredictedHR - (double)a.AverageHeartRate)));

            return averageError;

        }
    }
}