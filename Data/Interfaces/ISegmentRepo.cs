using Data.Models.Strava;

namespace Data.Interfaces
{
    public interface ISegmentRepo
    {
        void AddSegment(SummarySegment segment);
        void UpdateSegment(SummarySegment segment);
        long? AddOrEditSegment(SummarySegment segment);
    }
}