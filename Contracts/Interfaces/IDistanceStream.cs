namespace Contracts.Interfaces
{
    public interface IDistanceStream
    {
        List<float?> Data { get; set; }
        int? DistanceStreamId { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        long StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}