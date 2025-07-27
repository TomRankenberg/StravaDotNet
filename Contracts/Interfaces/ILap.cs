namespace Contracts.Interfaces
{
    public interface ILap
    {
        float? AverageCadence { get; set; }
        float? AverageSpeed { get; set; }
        float? Distance { get; set; }
        int? ElapsedTime { get; set; }
        int? EndIndex { get; set; }
        long? Id { get; set; }
        int? LapIndex { get; set; }
        float? MaxSpeed { get; set; }
        int? MovingTime { get; set; }
        string Name { get; set; }
        int? PaceZone { get; set; }
        int? Split { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? StartDateLocal { get; set; }
        int? StartIndex { get; set; }
        float? TotalElevationGain { get; set; }

        string ToJson();
        string ToString();
    }
}