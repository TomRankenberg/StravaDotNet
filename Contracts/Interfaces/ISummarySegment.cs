namespace Contracts.Interfaces
{
    public interface ISummarySegment
    {
        bool? _Private { get; set; }
        string? ActivityType { get; set; }
        float? AverageGrade { get; set; }
        string? City { get; set; }
        int? ClimbCategory { get; set; }
        string? Country { get; set; }
        float? Distance { get; set; }
        float? ElevationHigh { get; set; }
        float? ElevationLow { get; set; }
        long? Id { get; set; }
        float? MaximumGrade { get; set; }
        string? Name { get; set; }
        string? State { get; set; }

        string ToJson();
        string ToString();
    }
}