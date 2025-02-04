using Data.Interfaces;
using Data.Models.Strava;
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

            List<string> polylines = activities.Where(a => a.Type == activity).Select(a => a.Polyline).ToList();

            return Ok(polylines);
        }
    }
}
