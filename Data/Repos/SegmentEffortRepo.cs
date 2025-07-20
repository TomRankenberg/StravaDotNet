using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentEffortRepo(DatabaseContext context) : ISegmentEffortRepo
    {
        public void AddSegmentEffort(IDetailedSegmentEffort effort)
        {
            var entity = effort as DetailedSegmentEffort;
            context.SegmentEfforts.Add(entity);
            context.SaveChanges();
            context.Entry(effort).State = EntityState.Detached;
        }

        public void UpdateSegmentEffort(IDetailedSegmentEffort effort)
        {
            var entity = effort as DetailedSegmentEffort;   
            context.SegmentEfforts.Update(entity);
            context.SaveChangesAsync();
            context.Entry(effort).State = EntityState.Detached;
        }

        public void AddOrEditSegmentEffort(IDetailedSegmentEffort effort)
        {
            if (context.SegmentEfforts.Contains(effort))
            {
                UpdateSegmentEffort(effort);
            }
            else
            {
                AddSegmentEffort(effort);
            }
        }
    }
}
