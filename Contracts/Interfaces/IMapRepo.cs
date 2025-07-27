namespace Contracts.Interfaces
{
    public interface IMapRepo
    {
        void AddMap(IPolylineMap map);
        void UpdateMap(IPolylineMap map);
        string AddOrEditMap(IPolylineMap map);  
        IPolylineMap GetMapById(string id);
    }
}