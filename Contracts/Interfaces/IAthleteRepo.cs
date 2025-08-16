namespace Contracts.Interfaces
{
    public interface IAthleteRepo
    {
        Task AddAthleteAsync(IMetaAthlete athlete);
        Task UpdateAthleteAsync(IMetaAthlete athlete);
        Task AddOrEditAthlete(IMetaAthlete athlete);
    }
}