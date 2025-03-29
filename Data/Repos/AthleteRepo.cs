using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace Data.Repos
{
    public class AthleteRepo(DatabaseContext context) : IAthleteRepo
    {
        public void AddAthlete(MetaAthlete athlete)
        {
            context.MetaAthletes.Add(athlete);
            context.SaveChanges();
        }
        public void UpdateAthlete(MetaAthlete athlete)
        {
            context.MetaAthletes.Update(athlete);
            context.SaveChangesAsync();
        }
    }
}
