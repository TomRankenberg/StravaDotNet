using Contracts.Interfaces;
using Data.Models.Strava;
using DataManagement.Models;
using Microsoft.AspNetCore.Mvc;
using StravaDotNet.Components.Services;

namespace StravaDotNet.Controllers
{
    [Route("api/[controller]")]
    public class HeatmapController(IActivitiesRepo activitiesRepo, HeatmapService heatmapService) : ControllerBase
    {
        [HttpGet]
        [Route("GetHeatmap")]
        public IActionResult GetHeatmap(bool runs, bool rides)
        {
            // Retrieve all activities
            IQueryable<DetailedActivity> activities = (IQueryable<DetailedActivity>)activitiesRepo.GetAllActivities();

            // Filter activities based on the type (Run, Ride, or both)
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

            // Ensure there are activities with maps
            IQueryable<DetailedActivity> activitiesWithMaps = activities.Where(a => a.MapId != null);
            if (!activitiesWithMaps.Any())
            {
                return NotFound("No activities with maps found.");
            }

            // Use HeatmapService to generate heatmap data
            HeatMapData heatMapData = heatmapService.GetHeatmapData(activitiesWithMaps);

            return Ok(heatMapData);
        }
    }
}
