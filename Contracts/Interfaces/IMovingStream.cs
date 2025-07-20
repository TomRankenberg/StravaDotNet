namespace Contracts.Interfaces
{
    public interface IMovingStream
    {
        List<bool?> Data { get; set; }
        int? MovingStreamId { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}