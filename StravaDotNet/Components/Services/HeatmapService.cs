using Contracts.DTOs;
using Contracts.Interfaces;
using DataManagement.BusinessLogic;
using DataManagement.Models;

namespace StravaDotNet.Components.Services
{
    public class HeatmapService
    {
        private readonly IMapRepo _mapRepo;

        public HeatmapService(IMapRepo mapRepo)
        {
            _mapRepo = mapRepo;
        }

        public async Task<HeatMapData> GetHeatmapData(List<ActivityDTO> activities)
        {
            HeatMapData data = new()
            {
                Input = [],
                Count = activities.Count
            };

            List<ActivityDTO> activitiesList = activities.Where(a => a.MapId != null).ToList();

            List<Task<HeatmapInput>> inputTasks = activitiesList.Select(async activity =>
            {
                IPolylineMap map = await _mapRepo.GetMapById(activity.MapId);
                return new HeatmapInput
                {
                    ActivityType = activity.Type,
                    EncodedPolyline = map.SummaryPolyline ?? "",
                    StartPoint = Mappers.ConvertToLatLng(activity.StartLatlng),
                    EndPoint = Mappers.ConvertToLatLng(activity.EndLatlng),
                    StartTime = activity.StartDate,
                    LineOpacity = Math.Clamp(5.0 / activities.Count, 0.2, 1.0)
                };
            }).ToList();

            var inputs = await Task.WhenAll(inputTasks);
            data.Input.AddRange(inputs);

            return data;
        }
    }
}