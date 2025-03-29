using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentEffortRepo(DatabaseContext context) : ISegmentEffortRepo
    {
        public void AddSegmentEffort(DetailedSegmentEffort effort)
        {
            context.SegmentEfforts.Add(effort);
            context.SaveChanges();
            context.Entry(effort).State = EntityState.Detached;
        }

        public void UpdateSegmentEffort(DetailedSegmentEffort effort)
        {
            context.SegmentEfforts.Update(effort);
            context.SaveChangesAsync();
            context.Entry(effort).State = EntityState.Detached;
        }

        public void AddOrEditSegmentEffort(DetailedSegmentEffort effort)
        {
            if (context.SegmentEfforts.Contains(effort))
            {
                //UpdateSegmentEffort(effort);
            }
            else
            {
                AddSegmentEffort(effort);
            }
        }
    }
}
