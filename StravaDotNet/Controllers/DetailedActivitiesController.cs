using Microsoft.AspNetCore.Mvc;
using Data.Models.Strava;
using Data.Context;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailedActivitiesController(DatabaseContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<DetailedActivity>> Get()
        {
            return context.Activities.ToList();
        }
    }
}
