namespace Contracts.Interfaces
{
    public interface ISavingService
    {
        Task SaveActivities(string activitiesJson, string? token);
    }
}