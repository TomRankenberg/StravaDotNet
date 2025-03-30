using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentRepo(DatabaseContext context) : ISegmentRepo
    {
        public void AddSegment(SummarySegment segment)
        {
            context.Segments.Add(segment);
            context.SaveChanges();
            context.Entry(segment).State = EntityState.Detached;
        }
        public void UpdateSegment(SummarySegment segment)
        {
            context.Segments.Update(segment);
            context.SaveChangesAsync();
            context.Entry(segment).State = EntityState.Detached;
        }
        public long? AddOrEditSegment(SummarySegment segment)
        {
            if (context.Segments.Contains(segment))
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
