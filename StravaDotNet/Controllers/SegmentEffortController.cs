using Microsoft.AspNetCore.Mvc;
using Data.Models.Strava;
using Data.Context;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegmentEffortController(DatabaseContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<DetailedSegmentEffort>> Get()
        {
            return context.SegmentEfforts.ToList();
        }

        [HttpGet]
        [Route("GetPRs")]
        public ActionResult<List<DetailedSegmentEffort>> GetPRs()
        {
            string[] prNames = new string[] { "1 mile", "1/2 mile", "10K", "15K", "1K", "2 mile", "400m", "5K", "Half-Marathon" };

            return context.SegmentEfforts.Where(se => prNames.Contains(se.Name)).ToList();
        }
    }
}
