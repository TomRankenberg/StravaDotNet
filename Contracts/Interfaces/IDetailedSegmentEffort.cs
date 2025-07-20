namespace Contracts.Interfaces
{
    public interface IDetailedSegmentEffort
    {
        long? ActivityId { get; set; }
        float? AverageCadence { get; set; }
        float? AverageHeartrate { get; set; }
        float? AverageWatts { get; set; }
        IDetailedActivity DetailedActivity { get; set; }
        bool? DeviceWatts { get; set; }
        float? Distance { get; set; }
        int? ElapsedTime { get; set; }
        int? EndIndex { get; set; }
        bool? Hidden { get; set; }
        long? Id { get; set; }
        bool? IsKom { get; set; }
        int? KomRank { get; set; }
        float? MaxHeartrate { get; set; }
        int? MovingTime { get; set; }
        string Name { get; set; }
        int? PrRank { get; set; }
        ISummarySegment Segment { get; set; }
        long? SegmentId { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? StartDateLocal { get; set; }
        int? StartIndex { get; set; }

        string ToJson();
        string ToString();
    }
}