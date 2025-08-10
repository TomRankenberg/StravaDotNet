using Microsoft.AspNetCore.Mvc;
using Data.Context;
using StravaDotNet.ViewModels;
using Data.Models.Strava;
using DataManagement.BusinessLogic;
using Contracts.DTOs;

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
                DateTime fromUtc = from.Value.ToUniversalTime();
                query = query.Where(a => a.StartDateLocal.HasValue && a.StartDateLocal.Value >= fromUtc);
            }

            if (to.HasValue)
            {
                to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 23, 59, 59);
                DateTime toUtc = to.Value.ToUniversalTime();
                query = query.Where(a => a.StartDateLocal.HasValue && a.StartDateLocal.Value <= toUtc);
            }

            List<ActivityVm> activityVms = [];
            foreach (DetailedActivity activity in query)
            {
                ActivityVm activityVm = new()
                {
                    Activity = Mappers.MapToActivityDto(activity),
                    AverageHeartRate = 0 // Placeholder
                };
                activityVms.Add(activityVm);
            }
            return activityVms;
        }

        [HttpGet]
        [Route("GetAllActivities")]
        public ActionResult<List<ActivityDTO>> GetAllActivities()
        {
            List<ActivityDTO> activities = [];
            foreach (DetailedActivity activity in context.Activities)
            {
                ActivityDTO activityDTO= Mappers.MapToActivityDto(activity);
                activities.Add(activityDTO);
            }
            return activities;
        }
    }
}