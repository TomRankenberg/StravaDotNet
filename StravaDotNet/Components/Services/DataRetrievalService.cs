using Contracts.DTOs;
using Data.Models.Strava;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DataRetrievalService(
        DetailedActivityService detailedActivityService,
        SegmentEffortService segmentEffortService)
    {
        public async Task<List<ActivityVm>> GetAllActivityVmsAsync()
        {
            return await detailedActivityService.GetDetailedActivityVmsAsync();
        }

        public async Task<List<ActivityDTO>> GetAllActivitiesAsync()
        {
            return await detailedActivityService.GetDetailedActivitiesAsync();
        }

        public async Task<List<DetailedSegmentEffort>> GetAllSegmentEffortsAsync()
        {
            return await segmentEffortService.GetDetailedSegmentEffortsAsync();
        }
    }
}
