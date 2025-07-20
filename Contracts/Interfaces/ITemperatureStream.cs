namespace Contracts.Interfaces
{
    public interface ITemperatureStream
    {
        List<int?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? StreamSetId { get; set; }
        int? TemperatureStreamId { get; set; }

        string ToJson();
        string ToString();
    }
}