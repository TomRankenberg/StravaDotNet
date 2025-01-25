using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace Data.Repos
{
    public class ActivitiesRepo(DatabaseContext context) : IActivitiesRepo
    {
        public void AddActivity(DetailedActivity detailedActivity)
        {
            context.Activities.Add(detailedActivity);
            context.SaveChanges();
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
    }
}
