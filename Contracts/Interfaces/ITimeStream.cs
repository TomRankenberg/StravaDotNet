namespace Contracts.Interfaces
{
    public interface ITimeStream
    {
        List<int?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        long StreamSetId { get; set; }
        int? TimeStreamId { get; set; }

        string ToJson();
        string ToString();
    }
}