namespace DataManagement.Models
{
    public class ActivityForStats
    {
        public long? Id { get; set; }
        public string Type { get; set; } = "";
        public float? Distance { get; set; }
        public int? ElapsedTime { get; set; }
        public float? ElevationGain { get; set; }
        public DateTime? StartDate { get; set; }
        public float? AverageSpeed { get; set; }
        public double? ActiveRecentTimeAll { get; set; }// Seconds in the past 30d
        public double? ActiveRecentTimeThisType { get; set; }// Seconds in the past 30d
        public double? AverageHeartRate { get; set; }
    }
}
