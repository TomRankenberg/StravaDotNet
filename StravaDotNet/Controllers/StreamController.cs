using Data.Context;
using Data.Models.Strava;
using Microsoft.AspNetCore.Mvc;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreamController(DatabaseContext context) : ControllerBase
    {
        [HttpGet]
        [Route("GetHeartStreamFromActivityId")]
        public ActionResult<HeartrateStream> GetHeartStreamFromActivityId(long? id)
        {
            int? streamSetId = context.Streams
                .Where(s => s.ActivityId == id)
                .Select(s => s.StreamSetId)
                .FirstOrDefault();
            HeartrateStream? heartrateStream = context.HeartrateStreams
                .Where(h => h.StreamSetId == streamSetId)
                .FirstOrDefault();

            if (heartrateStream != null)
            {
                return Ok(heartrateStream);
            }
            else
            {
                return Ok(new HeartrateStream());
            }
        }
    }
}
