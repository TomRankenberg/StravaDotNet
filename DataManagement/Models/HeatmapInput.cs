namespace DataManagement.Models
{
    public class HeatmapInput
    {
        public string EncodedPolyline { get; set; } = "";
        public DateTime? StartTime { get; set; }
        public string ActivityType { get; set; } = "";
        public double LineOpacity { get; set; }
        public string LineColor { get; set; } = "";
    }

    public class HeatMapData
    {
        public List<HeatmapInput> Input { get; set; }
        public int Count { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }

        public HeatMapData()
        {
            Input = [];
        }
    }
}
