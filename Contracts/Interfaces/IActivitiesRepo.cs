namespace Contracts.Interfaces
{
    public interface IActivitiesRepo
    {
        public Task AddActivityAsync(IDetailedActivity detailedActivity);
        public Task AddOrEditActivity(IDetailedActivity detailedActivity);
        public IDetailedActivity GetActivityById(int id);
        public Task UpdateActivityAsync(IDetailedActivity detailedActivity);
        public List<long?> GetAllActivityIds();
        public List<IDetailedActivity> GetAllActivities();
        public List<IDetailedActivity> GetAllActivitiesNoTracking();
    }
}
