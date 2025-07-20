namespace Contracts.Interfaces
{
    public interface ICadenceStream
    {
        int? CadenceStreamId { get; set; }
        List<int?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}