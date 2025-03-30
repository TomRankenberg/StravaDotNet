using Data.Interfaces;
using Data.Models.Strava;
using DataManagement.BusinessLogic;
using DataManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace StravaDotNet.Controllers
{
    [Route("api/[controller]")]
    public class HeatmapController(IActivitiesRepo activitiesRepo, IMapRepo mapRepo) : ControllerBase
    {
        [HttpGet]
        [Route("GetHeatmap")]
        public IActionResult GetHeatmap(bool runs, bool rides)
        {
            List<DetailedActivity> activities = activitiesRepo.GetAllActivities();

            if (runs && rides)
            {
                activities = activities.Where(a => a.Type == "Run" || a.Type == "Ride").ToList();
            }
            else if (runs)
            {
                activities = activities.Where(a => a.Type == "Run").ToList();
            }
            else if (rides)
            {
                activities = activities.Where(a => a.Type == "Ride").ToList();
            }
            List<DetailedActivity> activitiesWithMaps = activities.Where(a => a.MapId != null).ToList();

            HeatMapData heatMapData = HeatmapManager.GetHeatmapData(activitiesWithMaps, mapRepo);

            return Ok(heatMapData);
        }
    }
}
