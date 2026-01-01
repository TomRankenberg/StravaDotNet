using Contracts.DTOs;
using Contracts.Interfaces;
using Data.Models.Strava;
using DataManagement.Extensions;
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
            foreach (ActivityDTO activity in activitiesList)
            {
                if (activity.MapId == null)
                {
                    continue;
                }
                IPolylineMap map = await mapRepo.GetMapByIdNoTracking(activity.MapId);
                inputs.Add(new HeatmapInput
                {
                    ActivityType = activity.Type,
                    EncodedPolyline = map.SummaryPolyline ?? "",
                    StartTime = activity.StartDate,
                    LineOpacity = Math.Clamp(5.0 / activities.Count, 0.2, 1.0)
                });
            }
            data.Input.AddRange(inputs);
            IEnumerable<float?> latitudes = activitiesList.Select(a => a.StartLatlng?[0]);
            IEnumerable<float?> longitudes = activitiesList.Select(a => a.StartLatlng?[1]);
            data.CenterLatitude = latitudes.Median() ?? 52.08064;
            data.CenterLongitude = longitudes.Median() ?? 5.12130;

            return data;
        }

        public async Task<HeatMapData> GetHeatmapDataSingle(string? mapId, string activityType, LatLng? latLng)
        {
            if (mapId == null)
            {
                return new HeatMapData();
            }

            IPolylineMap map = await mapRepo.GetMapByIdNoTracking(mapId);
            HeatmapInput input = new()
            {
                EncodedPolyline = map.SummaryPolyline ?? "",
                LineOpacity = 1.0,
                LineColor = "#f5f5f5",
                ActivityType = activityType
            };
            HeatMapData heatMapData = new()
            {
                Input = [input],
                Count = 1,
                CenterLatitude = latLng?[0] ?? 52.08064,
                CenterLongitude = latLng?[1] ?? 5.12130
            };

            return heatMapData;
        }
    }
}