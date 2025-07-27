namespace Contracts.Interfaces
{
    public interface ISmoothVelocityStream
    {
        List<float?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? SmoothVelocityStreamId { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}