namespace Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivitiesRepo Activities { get; }
        IAthleteRepo Athletes { get; }
        IMapRepo Maps { get; }
        ISegmentRepo Segments { get; }
        ISegmentEffortRepo SegmentEfforts { get; }
        IStreamSetRepo StreamSets { get; }
        ITimeStreamRepo TimeStreams { get; }
        IDistanceStreamRepo DistanceStreams { get; }
        ILatLngStreamRepo LatLngStreams { get; }
        ISmoothGradeStreamRepo SmoothGradeStreams { get; }
        IMovingStreamRepo MovingStreams { get; }
        ITemperatureStreamRepo TemperatureStreams { get; }
        IPowerStreamRepo PowerStreams { get; }
        ICadenceStreamRepo CadenceStreams { get; }
        IHeartrateStreamRepo HeartrateStreams { get; }
        ISmoothVelocityStreamRepo SmoothVelocityStreams { get; }
        IAltitudeStreamRepo AltitudeStreams { get; }

        Task<int> SaveChangesAsync();
    }

}
