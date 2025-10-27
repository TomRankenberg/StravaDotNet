namespace Contracts.Interfaces
{
    public interface IStravaService
    {
        Task<IStreamSet?> GetStreamsForActivity(long? id, string? token);
        Task<IDetailedActivity?> GetActivityById(long? activityId, string? token);
    }
}