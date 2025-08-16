namespace Contracts.Interfaces
{
    public interface ISmoothGradeStream
    {
        List<float?> Data { get; set; }
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }
        int? SmoothGradeId { get; set; }
        long StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}