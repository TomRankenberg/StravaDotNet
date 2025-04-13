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
            IQueryable<DetailedActivity> activities = activitiesRepo.GetAllActivities();

            if (runs && rides)
            {
                activities = activities.Where(a => a.Type == "Run" || a.Type == "Ride");
            }
            else if (runs)
            {
                activities = activities.Where(a => a.Type == "Run");
            }
            else if (rides)
            {
                activities = activities.Where(a => a.Type == "Ride");
            }
            IQueryable<DetailedActivity> activitiesWithMaps = activities.Where(a => a.MapId != null);

            if (!activitiesWithMaps.Any())
            {
                return NotFound("No activities with maps found.");
            }
            else
            {
                HeatMapData heatMapData = HeatmapManager.GetHeatmapData(activitiesWithMaps, mapRepo);

                return Ok(heatMapData);
            }
        }
    }
}
