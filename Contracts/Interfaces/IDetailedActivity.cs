namespace Contracts.Interfaces
{
    public interface IDetailedActivity
    {
        bool? _Private { get; set; }
        int? AchievementCount { get; set; }
        IMetaAthlete Athlete { get; set; }
        int? AthleteCount { get; set; }
        int AthleteId { get; set; }
        float? AverageSpeed { get; set; }
        float? AverageWatts { get; set; }
        List<IDetailedSegmentEffort> BestEfforts { get; set; }
        float? Calories { get; set; }
        int? CommentCount { get; set; }
        bool? Commute { get; set; }
        string? Description { get; set; }
        string? DeviceName { get; set; }
        bool? DeviceWatts { get; set; }
        float? Distance { get; set; }
        int? ElapsedTime { get; set; }
        float? ElevHigh { get; set; }
        float? ElevLow { get; set; }
        string? EmbedToken { get; set; }
        ILatLng EndLatlng { get; set; }
        string ExternalId { get; set; }
        bool? Flagged { get; set; }
        string? GearId { get; set; }
        bool? HasKudoed { get; set; }
        long? Id { get; set; }
        float? Kilojoules { get; set; }
        int? KudosCount { get; set; }
        List<ILap> Laps { get; set; }
        bool? Manual { get; set; }
        IPolylineMap Map { get; set; }
        string MapId { get; set; }
        float? MaxSpeed { get; set; }
        int? MaxWatts { get; set; }
        int? MovingTime { get; set; }
        string Name { get; set; }
        int? PhotoCount { get; set; }
        string? Polyline { get; set; }
        List<IDetailedSegmentEffort> SegmentEfforts { get; set; }
        List<ISplit> SplitsMetric { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? StartDateLocal { get; set; }
        ILatLng StartLatlng { get; set; }
        string Timezone { get; set; }
        float? TotalElevationGain { get; set; }
        int? TotalPhotoCount { get; set; }
        bool? Trainer { get; set; }
        string Type { get; set; }
        long? UploadId { get; set; }
        int? WeightedAverageWatts { get; set; }
        int? WorkoutType { get; set; }

        string ToJson();
        string ToString();
    }
}