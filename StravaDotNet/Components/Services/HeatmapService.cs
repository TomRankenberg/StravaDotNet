using Contracts.DTOs;
using Contracts.Interfaces;
using DataManagement.BusinessLogic;
using DataManagement.Models;

namespace StravaDotNet.Components.Services
{
    public class HeatmapService(IMapRepo mapRepo)
    {
        public async Task<HeatMapData> GetHeatmapData(List<ActivityDTO> activities)
        {
            HeatMapData data = new()
            {
                Input = [],
                Count = activities.Count
            };

            List<ActivityDTO> activitiesList = activities.Where(a => a.MapId != null).ToList();

            List<HeatmapInput> inputs = [];
            foreach (var activity in activitiesList)
            {
                IPolylineMap map = await mapRepo.GetMapByIdNoTracking(activity.MapId);
                inputs.Add(new HeatmapInput
                {
                    ActivityType = activity.Type,
                    EncodedPolyline = map.SummaryPolyline ?? "",
                    StartPoint = Mappers.ConvertToLatLng(activity.StartLatlng),
                    EndPoint = Mappers.ConvertToLatLng(activity.EndLatlng),
                    StartTime = activity.StartDate,
                    LineOpacity = Math.Clamp(5.0 / activities.Count, 0.2, 1.0)
                });
            }
            data.Input.AddRange(inputs);

            return data;
        }
    }
}