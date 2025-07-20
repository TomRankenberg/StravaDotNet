namespace Contracts.Interfaces
{
    public interface ISplit
    {
        int? _Split { get; set; }
        float? AverageSpeed { get; set; }
        IDetailedActivity DetailedActivity { get; set; }
        float? Distance { get; set; }
        int? ElapsedTime { get; set; }
        float? ElevationDifference { get; set; }
        int? MovingTime { get; set; }
        int? PaceZone { get; set; }

        string ToJson();
        string ToString();
    }
}