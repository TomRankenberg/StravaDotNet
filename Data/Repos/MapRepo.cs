using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class MapRepo(DatabaseContext context) : IMapRepo
    {
        public async Task AddMapAsync(IPolylineMap map)
        {
            var entity = map as PolylineMap;
            context.PolylineMaps.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMapAsync(IPolylineMap map)
        {
            var entity = map as PolylineMap;
            context.PolylineMaps.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<string> AddOrEditMap(IPolylineMap map)
        {
            if (context.PolylineMaps.Any(p => p.Id == map.Id))
            {
                await UpdateMapAsync(map);
            }
            else
            {
                await AddMapAsync(map);
            }
            return map.Id;
        }

        public async Task<IPolylineMap> GetMapById(string id)
        {
            return context.PolylineMaps.Find(id);
        }

        public async Task<IPolylineMap> GetMapByIdNoTracking(string id)
        {
            IPolylineMap map = await context.PolylineMaps.AsNoTracking().FirstAsync(m => m.Id == id);
            return map;
        }
    }
}
