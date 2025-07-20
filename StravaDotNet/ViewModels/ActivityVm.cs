using Contracts.DTOs;

namespace StravaDotNet.ViewModels
{
    public class ActivityVm
    {
        public ActivityDTO? Activity { get; set; }
        public double AverageHeartRate { get; set; }
        public double? PredictedHR { get; set; }
    }
}
