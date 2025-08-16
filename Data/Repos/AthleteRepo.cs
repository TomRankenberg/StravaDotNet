using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;

namespace Data.Repos
{
    public class AthleteRepo(DatabaseContext context) : IAthleteRepo
    {
        public async Task AddAthleteAsync(IMetaAthlete athlete)
        {
            var entity = athlete as MetaAthlete ;
            context.MetaAthletes.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAthleteAsync(IMetaAthlete athlete)
        {
            //context.MetaAthletes.Update(athlete);
            await context.SaveChangesAsync();
        }
        public async Task AddOrEditAthlete(IMetaAthlete athlete)
        {
            if (context.MetaAthletes.Any(a => a.Id == athlete.Id))
            {
                await UpdateAthleteAsync(athlete);
            }
            else
            {
                await AddAthleteAsync(athlete);
            }
        }
    }
}
