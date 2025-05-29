using DataManagement.Models;
using Data.Models.Strava;

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
    }
}
