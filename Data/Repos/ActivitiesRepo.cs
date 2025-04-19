using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ActivitiesRepo(DatabaseContext context) : IActivitiesRepo
    {
        
        public void AddActivity(DetailedActivity detailedActivity)
        {
            context.Activities.Add(detailedActivity);
            context.SaveChanges();
        }

        public void AddOrEditActivity(DetailedActivity detailedActivity)
        {
            if (context.Activities.Any(x => x.Id == detailedActivity.Id))
            {
                UpdateActivity(detailedActivity);
            }
            else
            {
                AddActivity(detailedActivity);
            }
            //DetachActivity(detailedActivity);
        }

        public DetailedActivity GetActivityById(int id)
        {
            return context.Activities.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateActivity(DetailedActivity detailedActivity)
        {
            var existingActivity = context.Activities.Local.FirstOrDefault(a => a.Id == detailedActivity.Id);
            if (existingActivity != null)
            {
                context.Entry(existingActivity).State = EntityState.Detached;
            }

            context.Activities.Attach(detailedActivity);
            context.Entry(detailedActivity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<long?> GetAllActivityIds()
        {
            return context.Activities.Select(s => s.Id).ToList();

        }

        public void DetachActivity(DetailedActivity detailedActivity)
        {
            context.Entry(detailedActivity).State = EntityState.Detached;
        }

        public IQueryable<DetailedActivity> GetAllActivities()
        {
            return context.Activities;
        }
    }
}