namespace Contracts.Interfaces
{
    public interface ISegmentRepo
    {
        Task AddSegmentAsync(ISummarySegment segment);
        Task UpdateSegmentAsync(ISummarySegment segment);
        Task<long?> AddOrEditSegment(ISummarySegment segment);
    }
}