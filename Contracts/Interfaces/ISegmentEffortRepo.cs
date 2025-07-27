namespace Contracts.Interfaces
{
    public interface ISegmentEffortRepo
    {
        public void AddSegmentEffort(IDetailedSegmentEffort segmentEffort);
        public void UpdateSegmentEffort(IDetailedSegmentEffort segmentEffort);
        public void AddOrEditSegmentEffort(IDetailedSegmentEffort segmentEffort);

    }
}