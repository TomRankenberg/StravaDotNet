using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ActivitiesRepo(DatabaseContext context) : IActivitiesRepo
    {
        
        public void AddActivity(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity;
            context.Activities.Add(entity);
            context.SaveChanges();
        }

        public void AddOrEditActivity(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity; 
            if (context.Activities.Any(x => x.Id == detailedActivity.Id))
            {
                UpdateActivity(entity);
            }
            else
            {
                AddActivity(entity);
            }
            //DetachActivity(detailedActivity);
        }

        public IDetailedActivity GetActivityById(int id)
        {
            return context.Activities.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateActivity(IDetailedActivity detailedActivity)
        {
            var entity = detailedActivity as DetailedActivity;
            var existingActivity = context.Activities.Local.FirstOrDefault(a => a.Id == detailedActivity.Id);
            if (existingActivity != null)
            {
                context.Entry(existingActivity).State = EntityState.Detached;
            }

            context.Activities.Attach(entity);
            context.Entry(detailedActivity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<long?> GetAllActivityIds()
        {
            return context.Activities.Select(s => s.Id).ToList();

        }

        public void DetachActivity(IDetailedActivity detailedActivity)
        {
            context.Entry(detailedActivity).State = EntityState.Detached;
        }

        public IQueryable<IDetailedActivity> GetAllActivities()
        {
            return context.Activities;
        }
    }
}