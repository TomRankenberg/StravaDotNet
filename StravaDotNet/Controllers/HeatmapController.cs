using Data.Interfaces;
using Data.Models.Strava;
using DataManagement.BusinessLogic;
using DataManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace StravaDotNet.Controllers
{
    [Route("api/[controller]")]
    public class HeatmapController(IActivitiesRepo activitiesRepo) : ControllerBase
    {
        [HttpGet]
        [Route("GetHeatmap")]
        public IActionResult GetHeatmap(string activity)
        {
            List<DetailedActivity> activities = activitiesRepo.GetAllActivities();
            HeatMapData heatMapData = HeatmapManager.ProcessHeatmapData(activities);

            heatMapData.Input = heatMapData.Input.Where(a => a.ActivityType == activity).ToList();

            return Ok(heatMapData);
        }
    }
}
