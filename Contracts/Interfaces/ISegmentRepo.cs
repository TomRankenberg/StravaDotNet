namespace Contracts.Interfaces
{
    public interface ISegmentRepo
    {
        void AddSegment(ISummarySegment segment);
        void UpdateSegment(ISummarySegment segment);
        long? AddOrEditSegment(ISummarySegment segment);
    }
}