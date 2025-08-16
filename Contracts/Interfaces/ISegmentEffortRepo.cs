namespace Contracts.Interfaces
{
    public interface ISegmentEffortRepo
    {
        public Task AddSegmentEffortAsync(IDetailedSegmentEffort segmentEffort);
        public Task UpdateSegmentEffortAsync(IDetailedSegmentEffort segmentEffort);
        public Task AddOrEditSegmentEffortAsync(IDetailedSegmentEffort segmentEffort);

    }
}