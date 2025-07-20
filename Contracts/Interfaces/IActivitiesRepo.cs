namespace Contracts.Interfaces
{
    public interface IActivitiesRepo
    {
        public void AddActivity(IDetailedActivity detailedActivity);
        public void AddOrEditActivity(IDetailedActivity detailedActivity);
        public IDetailedActivity GetActivityById(int id);
        public void UpdateActivity(IDetailedActivity detailedActivity);
        public void DetachActivity(IDetailedActivity detailedActivity);
        public List<long?> GetAllActivityIds();
        public List<IDetailedActivity> GetAllActivities();
    }
}
