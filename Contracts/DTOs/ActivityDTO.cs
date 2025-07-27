namespace Contracts.DTOs
{
    public class ActivityDTO
    {
        public long? Id { get; set; }
        public string Type { get; set; } = "";
        public float? Distance { get; set; }// Meters
        public int? ElapsedTime { get; set; }// Seconds
        public float? ElevationGain { get; set; }// Meters
        public DateTime? StartDate { get; set; }
        public float? AverageSpeed { get; set; }// m/s
        public int? MovingTime { get; set; }
        public float? TotalElevationGain { get; set; }
        public LatLngDTO? StartLatlng { get; set; }
        public LatLngDTO? EndLatlng { get; set; }
        public string? MapId { get; set; }
        public string Name { get; set; } = "";
    }
}
