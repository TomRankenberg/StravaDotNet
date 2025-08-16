using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SegmentEffortRepo(DatabaseContext context) : ISegmentEffortRepo
    {
        public async Task AddSegmentEffortAsync(IDetailedSegmentEffort effort)
        {
            var entity = effort as DetailedSegmentEffort;
            context.SegmentEfforts.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(effort).State = EntityState.Detached;
        }

        public async Task UpdateSegmentEffortAsync(IDetailedSegmentEffort effort)
        {
            var entity = effort as DetailedSegmentEffort;   
            context.SegmentEfforts.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(effort).State = EntityState.Detached;
        }

        public async Task AddOrEditSegmentEffortAsync(IDetailedSegmentEffort effort)
        {
            if (context.SegmentEfforts.Any(e => e.Id == effort.Id))
            {
                await UpdateSegmentEffortAsync(effort);
            }
            else
            {
                await AddSegmentEffortAsync(effort);
            }
        }
    }
}
