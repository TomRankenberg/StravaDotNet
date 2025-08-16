using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ActivitiesRepo(DatabaseContext context) : IActivitiesRepo
    {
        
        public async Task AddActivityAsync(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity;
            context.Activities.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddOrEditActivity(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity; 
            if (context.Activities.Any(x => x.Id == detailedActivity.Id))
            {
                await UpdateActivityAsync(entity);
            }
            else
            {
                await AddActivityAsync(entity);
            }
        }

        public IDetailedActivity GetActivityById(int id)
        {
            return context.Activities.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateActivityAsync(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity;
            var existingActivity = context.Activities.Local.FirstOrDefault(a => a.Id == detailedActivity.Id);
            if (existingActivity != null)
            {
                context.Entry(existingActivity).State = EntityState.Detached;
            }

            context.Activities.Attach(entity);
            context.Entry(detailedActivity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public List<long?> GetAllActivityIds()
        {
            return context.Activities.Select(s => s.Id).ToList();

        }

        public List<IDetailedActivity> GetAllActivities()
        {
            return [..context.Activities];
        }
    }
}