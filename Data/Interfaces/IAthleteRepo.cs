using Data.Models.Strava;
using Strava.Models;

namespace Data.Interfaces
{
    public interface IAthleteRepo
    {
        void AddAthlete(MetaAthlete athlete);
        void UpdateAthlete(MetaAthlete athlete);
        void AddOrEditAthlete(MetaAthlete athlete);
    }
}