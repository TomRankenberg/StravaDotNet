using Contracts.DTOs;
using Contracts.Interfaces;
using Data.Models.Strava;
using DataManagement.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailedActivitiesController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        [Route("GetActivityVms")]
        public ActionResult<List<ActivityVm>> GetActivityVms([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            IEnumerable<IDetailedActivity> activities = unitOfWork.Activities.GetAllActivitiesNoTracking();

            if (from.HasValue)
            {
                DateTime fromUtc = from.Value.ToUniversalTime();
                activities = activities.Where(a => a.StartDateLocal.HasValue && a.StartDateLocal.Value >= fromUtc);
            }

            if (to.HasValue)
            {
                to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 23, 59, 59);
                DateTime toUtc = to.Value.ToUniversalTime();
                activities = activities.Where(a => a.StartDateLocal.HasValue && a.StartDateLocal.Value <= toUtc);
            }

            List<ActivityVm> activityVms = activities
                    .AsParallel()
                    .Select(activity => new ActivityVm
                    {
                        Activity = Mappers.MapToActivityDto((DetailedActivity)activity),
                        AverageHeartRate = 0 // Placeholder
                    })
                    .ToList();

            return activityVms;
        }

        [HttpGet]
        [Route("GetAllActivities")]
        public ActionResult<List<ActivityDTO>> GetAllActivities()
        {
            var activities = unitOfWork.Activities.GetAllActivitiesNoTracking();
            var activityDTOs = activities
                .Select(activity => Mappers.MapToActivityDto((DetailedActivity)activity))
                .ToList();

            return activityDTOs;
        }
    }
}