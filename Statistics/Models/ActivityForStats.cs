namespace Statistics.Models
{
    public class ActivityForStats
    {
        public long? Id { get; set; }
        public string Type { get; set; } = "";
        public float? Distance { get; set; }// Meters
        public int? ElapsedTime { get; set; }// Seconds
        public float? ElevationGain { get; set; }// Meters
        public DateTime? StartDate { get; set; }
        public float? AverageSpeed { get; set; }// m/s
        public double? ActiveRecentTimeAll { get; set; }// Seconds in the past 30d
        public double? ActiveRecentTimeThisType { get; set; }// Seconds in the past 30d
        public double? AverageHeartRate { get; set; }
        public double? PredictedHR { get; set; } // Predicted heart rate based on the model
    }
}
