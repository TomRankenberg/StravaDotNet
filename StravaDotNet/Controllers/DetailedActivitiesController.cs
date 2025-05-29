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
        [Route("GetActivityVms")]
        public ActionResult<List<ActivityVm>> GetActivityVms()
        {
            List<ActivityVm> activityVms = [];
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

        [HttpGet]
        [Route("GetAllActivities")]
        public ActionResult<List<DetailedActivity>> GetAllActivities()
        {
            List<DetailedActivity> activities = [];
            foreach (DetailedActivity activity in context.Activities)
            {
                activities.Add(activity);
            }
            return activities;
        }
    }
}