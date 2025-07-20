namespace Contracts.Interfaces
{
    public interface IPowerStream
    {
        List<int?> Data { get; set; }
        int? OriginalSize { get; set; }
        int? PowerStreamId { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}