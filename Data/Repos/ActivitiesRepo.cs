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
        }


        public DetailedActivity GetActivityById(int id)
        {
            return context.Activities.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateActivity(DetailedActivity detailedActivity)
        {
            context.Activities.Update(detailedActivity);
            context.SaveChanges();
        }

        public List<int> GetAllActivityIds()
        {
            List<int> ids = [];
            foreach (var activity in context.Activities)
            {
                int id = (int)activity.Id;
                ids.Add(id);
            }
            return ids;
        }

        public void DetachActivity(DetailedActivity detailedActivity)
        {
            context.Entry(detailedActivity).State = EntityState.Detached;
        }

        public List<DetailedActivity> GetAllActivities()
        {
            return context.Activities.ToList();
        }
    }
}