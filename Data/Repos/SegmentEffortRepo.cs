using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace Data.Repos
{
    public class SegmentEffortRepo(DatabaseContext context) : ISegmentEffortRepo
    {
        public void AddSegmentEffort(DetailedSegmentEffort effort)
        {
            context.SegmentEfforts.Add(effort);
            context.SaveChanges();
        }

        public void UpdateSegmentEffort(DetailedSegmentEffort effort)
        {
            context.SegmentEfforts.Update(effort);
            context.SaveChangesAsync();
        }
    }
}
