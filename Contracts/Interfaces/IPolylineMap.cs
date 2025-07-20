namespace Contracts.Interfaces
{
    public interface IPolylineMap
    {
        long? ActivityId { get; set; }
        string Id { get; set; }
        string? Polyline { get; set; }
        string? SummaryPolyline { get; set; }

        string ToJson();
        string ToString();
    }
}