namespace Contracts.Interfaces
{
    public interface IAthleteRepo
    {
        void AddAthlete(IMetaAthlete athlete);
        void UpdateAthlete(IMetaAthlete athlete);
        void AddOrEditAthlete(IMetaAthlete athlete);
    }
}