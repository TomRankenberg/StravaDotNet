using Data.Models.Strava;

namespace Data.Interfaces
{
    public interface ISegmentEffortRepo
    {
        public void AddSegmentEffort(DetailedSegmentEffort segmentEffort);
        public void UpdateSegmentEffort(DetailedSegmentEffort segmentEffort);
        public void AddOrEditSegmentEffort(DetailedSegmentEffort segmentEffort);

    }
}