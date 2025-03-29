using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace Data.Repos
{
    public class SegmentRepo(DatabaseContext context) : ISegmentRepo
    {
        public void AddSegment(SummarySegment segment)
        {
            context.Segments.Add(segment);
            context.SaveChanges();
        }
        public void UpdateSegment(SummarySegment segment)
        {
            context.Segments.Update(segment);
            context.SaveChangesAsync();
        }
    }
}
