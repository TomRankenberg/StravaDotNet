namespace Contracts.Interfaces
{
    public interface ILatLngStream
    {
        string Data { get; set; }
        int? LatLngStreamId { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}