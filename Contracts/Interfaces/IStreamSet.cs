namespace Contracts.Interfaces
{
    public interface IStreamSet
    {
        long? ActivityId { get; set; }
        long StreamSetId { get; set; }

        string ToJson();
        string ToString();
    }
}