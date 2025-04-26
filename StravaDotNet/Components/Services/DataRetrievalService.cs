using Data.Models.Strava;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DataRetrievalService
    {
        private readonly DetailedActivityService _detailedActivityService;
        private readonly SegmentEffortService _segmentEffortService;

        public DataRetrievalService(DetailedActivityService detailedActivityService, SegmentEffortService segmentEffortService)
        {
            _detailedActivityService = detailedActivityService;
            _segmentEffortService = segmentEffortService;
        }

        public async Task<List<ActivityVm>> GetAllActivitiesAsync()
        {
            return await _detailedActivityService.GetDetailedActivitiesAsync();
        }

        public async Task<List<DetailedSegmentEffort>> GetAllSegmentEffortsAsync()
        {
            return _segmentEffortService.GetDetailedSegmentEffortsAsync();
        }

        public object GetScatterChartDataForEfforts(List<DetailedSegmentEffort> efforts)
        {
            return _segmentEffortService.GetMonthlyScatterChartData(efforts);
        }
    }

}
