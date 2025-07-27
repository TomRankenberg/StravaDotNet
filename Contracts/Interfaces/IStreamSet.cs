namespace Contracts.Interfaces
{
    public interface IStreamSet
    {
        long? ActivityId { get; set; }
        int? StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}