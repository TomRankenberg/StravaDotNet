using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace Data.Repos
{
    public class MapRepo(DatabaseContext context) : IMapRepo
    {
        public void AddMap(PolylineMap map)
        {
            context.PolylineMaps.Add(map);
            context.SaveChanges();
        }

        public void UpdateMap(PolylineMap map)
        {
            context.PolylineMaps.Update(map);
            context.SaveChangesAsync();
        }
    }
}
