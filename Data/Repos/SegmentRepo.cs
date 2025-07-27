using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentRepo(DatabaseContext context) : ISegmentRepo
    {
        public void AddSegment(ISummarySegment segment)
        {
            var entity = segment as SummarySegment;
            context.Segments.Add(entity);
            context.SaveChanges();
            context.Entry(segment).State = EntityState.Detached;
        }
        public void UpdateSegment(ISummarySegment segment)
        {
            var entity = segment as SummarySegment;
            context.Segments.Update(entity);
            context.SaveChangesAsync();
            context.Entry(segment).State = EntityState.Detached;
        }
        public long? AddOrEditSegment(ISummarySegment segment)
        {
            if (context.Segments.Any(s => s.Id == segment.Id))
            {
                UpdateSegment(segment);
            }
            else
            {
                AddSegment(segment);
            }

            return segment.Id;
        }
    }
}
