using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;

namespace Data.Repos
{
    public class MapRepo(DatabaseContext context) : IMapRepo
    {
        public void AddMap(IPolylineMap map)
        {
            var entity = map as PolylineMap;
            context.PolylineMaps.Add(entity);
            context.SaveChanges();
        }

        public void UpdateMap(IPolylineMap map)
        {
            var entity = map as PolylineMap;
            context.PolylineMaps.Update(entity);
            context.SaveChangesAsync();
        }

        public string AddOrEditMap(IPolylineMap map)
        {
            if (context.PolylineMaps.Any(p => p.Id == map.Id))
            {
                UpdateMap(map);
            }
            else
            {
                AddMap(map);
            }
            return map.Id;
        }

        public IPolylineMap GetMapById(string id)
        {
            return context.PolylineMaps.Find(id);
        }
    }
}
