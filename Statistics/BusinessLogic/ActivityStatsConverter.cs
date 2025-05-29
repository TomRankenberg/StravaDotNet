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
    }
}
