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
        public ActionResult<List<ActivityVm>> GetActivityVms([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            var query = context.Activities.AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(a => a.StartDate.HasValue && a.StartDate.Value >= from.Value);
            }

            if (to.HasValue)
            {
                to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 23, 59, 59);
                query = query.Where(a => a.StartDate.HasValue && a.StartDate.Value <= to.Value);
            }

            List<ActivityVm> activityVms = [];
            foreach (DetailedActivity activity in query)
            {
                ActivityVm activityVm = new()
                {
                    Activity = activity,
                    AverageHeartRate = 0 // Placeholder
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