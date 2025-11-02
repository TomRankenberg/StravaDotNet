namespace Contracts.Interfaces
{
    public interface IStravaService
    {
        Task<IStreamSet?> GetStreamsForActivity(long? id, string? token);
        Task<string?> GetActivityById(long? activityId, string? token);
        Task<string?> RetrieveActivities(string? token, int? after);
    }
}