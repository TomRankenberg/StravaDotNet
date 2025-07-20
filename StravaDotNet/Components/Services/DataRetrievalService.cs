using Contracts.DTOs;
using Data.Models.Strava;
using DataManagement.Models;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DataRetrievalService
    {
        private readonly DetailedActivityService _detailedActivityService;
        private readonly SegmentEffortService _segmentEffortService;
        private readonly HeatmapService _heatmapService;

        public DataRetrievalService(
            DetailedActivityService detailedActivityService,
            SegmentEffortService segmentEffortService,
            HeatmapService heatmapService)
        {
            _detailedActivityService = detailedActivityService;
            _segmentEffortService = segmentEffortService;
            _heatmapService = heatmapService;
        }

        public async Task<List<ActivityVm>> GetAllActivityVmsAsync()
        {
            return await _detailedActivityService.GetDetailedActivityVmsAsync();
        }

        public async Task<List<ActivityDTO>> GetAllActivitiesAsync()
        {
            return await _detailedActivityService.GetDetailedActivitiesAsync();
        }

        public async Task<List<DetailedSegmentEffort>> GetAllSegmentEffortsAsync()
        {
            return await _segmentEffortService.GetDetailedSegmentEffortsAsync();
        }

        public HeatMapData GetHeatmapData(List<ActivityDTO> activities)
        {
            return _heatmapService.GetHeatmapData(activities);
        }
    }
}
