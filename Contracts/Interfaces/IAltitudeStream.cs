namespace Contracts.Interfaces
{
    public interface IAltitudeStream
    {
        int? AltitudeStreamId { get; set; }
        List<float?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        long StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}