namespace Contracts.Interfaces
{
    public interface IBaseStream
    {
        int? OriginalSize { get; set; }
        string Resolution { get; set; }
        string SeriesType { get; set; }

        string ToJson();
        string ToString();
    }
}