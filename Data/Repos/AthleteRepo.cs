using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;

namespace Data.Repos
{
    public class AthleteRepo(DatabaseContext context) : IAthleteRepo
    {
        public void AddAthlete(IMetaAthlete athlete)
        {
            var entity = athlete as MetaAthlete ;
            context.MetaAthletes.Add(entity);
            context.SaveChanges();
        }
        public void UpdateAthlete(IMetaAthlete athlete)
        {
            //context.MetaAthletes.Update(athlete);
            context.SaveChangesAsync();
        }
        public void AddOrEditAthlete(IMetaAthlete athlete)
        {
            if (context.MetaAthletes.Contains(athlete))
            {
                UpdateAthlete(athlete);
            }
            else
            {
                AddAthlete(athlete);
            }
        }
    }
}
