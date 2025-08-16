using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentRepo(DatabaseContext context) : ISegmentRepo
    {
        public async Task AddSegmentAsync(ISummarySegment segment)
        {
            var entity = segment as SummarySegment;
            context.Segments.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(segment).State = EntityState.Detached;
        }
        public async Task UpdateSegmentAsync(ISummarySegment segment)
        {
            var entity = segment as SummarySegment;
            context.Segments.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(segment).State = EntityState.Detached;
        }
        public async Task<long?> AddOrEditSegment(ISummarySegment segment)
        {
            if (context.Segments.Any(s => s.Id == segment.Id))
            {
                await UpdateSegmentAsync(segment);
            }
            else
            {
                await AddSegmentAsync(segment);
            }

            return segment.Id;
        }
    }
}
