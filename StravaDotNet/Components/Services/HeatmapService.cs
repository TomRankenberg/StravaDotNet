using Contracts.DTOs;
using Contracts.Interfaces;
using Data.Models.Strava;
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

        public HeatMapData GetHeatmapData(List<ActivityDTO> activities)
        {
            HeatMapData data = new()
            {
                Input = [],
                Count = activities.Count
            };

            List<ActivityDTO> activitiesList = activities.Where(a => a.MapId != null).ToList();
            foreach (ActivityDTO activity in activitiesList)
            {
                PolylineMap map = (PolylineMap)_mapRepo.GetMapById(activity.MapId);
                HeatmapInput input = new()
                {
                    ActivityType = activity.Type,
                    EncodedPolyline = map.SummaryPolyline ?? "",
                    StartPoint = Mappers.ConvertToLatLng(activity.StartLatlng),
                    EndPoint = Mappers.ConvertToLatLng(activity.EndLatlng),
                    StartTime = activity.StartDate,
                    LineOpacity = Math.Clamp(5.0 / activities.Count(), 0.2, 1.0)
                };
                data.Input.Add(input);
            }

            return data;
        }
    }
}
