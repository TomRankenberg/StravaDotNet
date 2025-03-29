using Data.Models.Strava;

namespace Data.Interfaces
{
    public interface IActivitiesRepo
    {
        public void AddActivity(DetailedActivity detailedActivity);
        public void AddOrEditActivity(DetailedActivity detailedActivity);
        public DetailedActivity GetActivityById(int id);
        public void UpdateActivity(DetailedActivity detailedActivity);
        public void DetachActivity(DetailedActivity detailedActivity);
        public List<int> GetAllActivityIds();
        public List<DetailedActivity> GetAllActivities();
    }
}
