using Data.Interfaces;
using Data.Models.Strava;
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

        public HeatMapData GetHeatmapData(IQueryable<DetailedActivity> activities)
        {
            HeatMapData data = new HeatMapData
            {
                Input = new List<HeatmapInput>(),
                Count = activities.Count()
            };

            List<DetailedActivity> activitiesList = activities.Where(a => a.MapId != null).ToList();
            foreach (DetailedActivity activity in activitiesList)
            {
                PolylineMap map = _mapRepo.GetMapById(activity.MapId);
                HeatmapInput input = new HeatmapInput
                {
                    ActivityType = activity.Type,
                    EncodedPolyline = map.SummaryPolyline ?? "",
                    StartPoint = activity.StartLatlng,
                    EndPoint = activity.EndLatlng,
                    StartTime = activity.StartDateLocal,
                    LineOpacity = Math.Clamp(5.0 / activities.Count(), 0.2, 1.0)
                };
                data.Input.Add(input);
            }

            return data;
        }
    }
}
