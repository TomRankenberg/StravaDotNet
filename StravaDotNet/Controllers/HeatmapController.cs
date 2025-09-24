using Contracts.DTOs;
using Contracts.Interfaces;
using Data.Models.Strava;
using DataManagement.BusinessLogic;
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
        public async Task<IActionResult> GetHeatmap(bool runs, bool rides)
        {
            // Retrieve all activities
            IEnumerable<IDetailedActivity> activities = activitiesRepo.GetAllActivitiesNoTracking();

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
            List<ActivityDTO> activitiesWithMaps = Mappers.MapToActivityDtos(activities.Where(a => a.MapId != null).ToList());
            if (activitiesWithMaps.Count == 0)
            {
                return NotFound("No activities with maps found.");
            }

            // Use HeatmapService to generate heatmap data
            HeatMapData heatMapData = await heatmapService.GetHeatmapData(activitiesWithMaps);

            return Ok(heatMapData);
        }
    }
}
