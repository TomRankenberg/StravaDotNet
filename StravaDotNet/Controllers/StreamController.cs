using Data.Context;
using Data.Models.Strava;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            HeartrateStream? heartrateStream = context.HeartrateStreams
                .Where(h => h.StreamSet.ActivityId == id)
                .FirstOrDefault();

            return Ok(heartrateStream ?? new HeartrateStream());
        }

        [HttpPost]
        [Route("GetHeartStreamsFromActivityIds")]
        public async Task<ActionResult<Dictionary<long, HeartrateStream>>> GetHeartStreamsFromActivityIds([FromBody] List<long> activityIds)
        {
            Dictionary<long, HeartrateStream> heartrateStreams = await context.HeartrateStreams
                .Join(
                    context.Streams,
                    heartrate => heartrate.StreamSetId,
                    streamSet => streamSet.StreamSetId,
                    (heartrate, streamSet) => new { Heartrate = heartrate, ActivityId = streamSet.ActivityId }
                )
                .Where(x => x.ActivityId.HasValue && activityIds.Contains(x.ActivityId.Value))
                .ToDictionaryAsync(x => x.ActivityId!.Value, x => x.Heartrate);

            var result = new Dictionary<long, HeartrateStream>();
            foreach (long id in activityIds)
            {
                if (heartrateStreams.TryGetValue(id, out HeartrateStream? stream))
                {
                    result[id] = stream;
                }
                else
                {
                    result[id] = new HeartrateStream();
                }
            }

            return Ok(result);
        }
    }
}
