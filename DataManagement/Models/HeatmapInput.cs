using Data.Models.Strava;

namespace DataManagement.Models
{
    public class HeatmapInput
    {
        public string EncodedPolyline { get; set; }
        public LatLng StartPoint { get; set; }
        public LatLng EndPoint { get; set; }
        public DateTime? StartTime { get; set; }
        public string StartLocation { get; set; }
        public string ActivityType { get; set; }
    }

    public class HeatMapData
    {
        public List<HeatmapInput> Input { get; set; }
        public int Count { get; set; }

    }
}
