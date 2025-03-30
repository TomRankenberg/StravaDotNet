using Data.Models.Strava;

namespace Data.Interfaces
{
    public interface IMapRepo
    {
        void AddMap(PolylineMap map);
        void UpdateMap(PolylineMap map);
        string AddOrEditMap(PolylineMap map);  
        PolylineMap GetMapById(string id);
    }
}