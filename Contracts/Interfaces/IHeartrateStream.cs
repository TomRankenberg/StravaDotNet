namespace Contracts.Interfaces
{
    public interface IHeartrateStream
    {
        List<int?> Data { get; set; }
        int? HeartrateStreamId { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        long StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}