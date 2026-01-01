using Data.Models.Strava;

namespace StravaDotNet.ViewModels
{
    public class SimpleActivityVm
    {
        public string Type { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public string Distance { get; set; } = "";
        public string? MapId { get; set; }
        public LatLng? StartLatLng { get; set; }
    }
}
