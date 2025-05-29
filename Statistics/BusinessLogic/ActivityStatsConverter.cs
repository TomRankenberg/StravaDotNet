using Data.Models.Strava;
using Statistics.Models;

namespace Statistics.BusinessLogic
{
    public class ActivityStatsConverter
    {

        public static ActivityForStats ConvertToActivityForStats(DetailedActivity activity, double? avgHeartRate)
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
                activity.PredictedHR = CalcPredictedHR(activity.ActiveRecentTimeAll, activity.AverageSpeed, activity.Distance, activity.Type);

            }

            return activities;
        }

        private static double? CalcPredictedHR(double? activeRecentAll, double? avgSpeed, double? distance, string type)
        {
            const double intercept = 4.480192e+01;
            const double speedCoefficient = 1.326652e+01;
            const double distanceCoefficient = 1.929494e-04;
            const double recentTimeCoefficient = -3.813923e-05;
            const double typeRunCoefficient = 7.925252e+01;
            const double typeSwimCoefficient = 9.391313e+01;

            if (activeRecentAll == null || avgSpeed == null || distance == null)
            {
                return null;
            }

            var typeCoefficient = type.ToLower() switch
            {
                "run" => typeRunCoefficient,
                "swim" => typeSwimCoefficient,
                _ => 1.0,// Default coefficient for rides
            };
            double predictedHR = intercept +
                          speedCoefficient * (avgSpeed ?? 0) +
                          distanceCoefficient * (distance ?? 0) +
                          recentTimeCoefficient * (activeRecentAll ?? 0) +
                          typeCoefficient;

            return predictedHR;
        }
    }
}
