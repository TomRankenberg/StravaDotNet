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
            foreach(ActivityForStats activity in activities)
            {
                activity.PredictedHR = CalcPredictedHR(activity);

            }

            return activities;
        }

        private static double? CalcPredictedHR(ActivityForStats activity)
        {
            const double intercept = 2.895563e+01;
            const double speedCoefficient = 1.590361e+01;
            const double distanceCoefficient = 9.037392e-06;
            const double recentTimeCoefficient = -3.274816e-05;
            const double typeRunCoefficient = 8.731897e+01;
            const double typeSwimCoefficient = 1.051025e+02;
            const double breakCoefficient = 1.296750e-02;

            if (activity.ActiveRecentTimeAll == null || activity.AverageSpeed == null || activity.Distance == null)
            {
                return null;
            }

            var typeCoefficient = activity.Type.ToLower() switch
            {
                "run" => typeRunCoefficient,
                "swim" => typeSwimCoefficient,
                _ => 1.0,// Default coefficient for rides
            };
            double predictedHR = intercept +
                          speedCoefficient * (activity.AverageSpeed ?? 0) +
                          distanceCoefficient * (activity.Distance ?? 0) +
                          recentTimeCoefficient * (activity.ActiveRecentTimeAll ?? 0) +
                          typeCoefficient +
                          breakCoefficient * (activity.BreakTime ?? 0);

            return predictedHR;
        }
    }
}
