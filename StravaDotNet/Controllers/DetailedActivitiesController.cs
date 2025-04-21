using Microsoft.AspNetCore.Mvc;
using Data.Context;
using StravaDotNet.ViewModels;
using Data.Models.Strava;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailedActivitiesController(DatabaseContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<ActivityVm>> Get()
        {
            List<ActivityVm> activityVms = new List<ActivityVm>();
            foreach (DetailedActivity activity in context.Activities)
            {
                ActivityVm activityVm = new()
                {
                    Activity = activity,
                    AverageHeartRate = 0 // Placeholder for average heart rate calculation
                };
                activityVms.Add(activityVm);
            }
            return activityVms;
        }
    }
}