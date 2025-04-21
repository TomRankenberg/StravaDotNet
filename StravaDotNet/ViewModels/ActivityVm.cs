using Data.Models.Strava;

namespace StravaDotNet.ViewModels
{
    public class ActivityVm
    {
        public DetailedActivity Activity { get; set; }
        public double AverageHeartRate { get; set; }
    }
}
