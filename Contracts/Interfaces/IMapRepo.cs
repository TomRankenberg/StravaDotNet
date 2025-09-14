namespace Contracts.Interfaces
{
    public interface IMapRepo
    {
        Task AddMapAsync(IPolylineMap map);
        Task UpdateMapAsync(IPolylineMap map);
        Task<string> AddOrEditMap(IPolylineMap map);  
        Task<IPolylineMap> GetMapById(string id);
    }
}