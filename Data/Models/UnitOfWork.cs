using Contracts.Interfaces;
using Data.Context;

namespace Data.Models;
public class UnitOfWork(DatabaseContext context,
                  IActivitiesRepo activitiesRepo,
                  IAthleteRepo athleteRepo,
                  IMapRepo mapRepo,
                  ISegmentRepo segmentRepo,
                  ISegmentEffortRepo segmentEffortRepo,
                  IStreamSetRepo streamSetRepo,
                ITimeStreamRepo timeStreamRepo,
                IDistanceStreamRepo distanceStreamRepo,
                ILatLngStreamRepo latLngStreamRepo,
                ISmoothGradeStreamRepo smoothGradeStreamRepo,
                IMovingStreamRepo movingStreamRepo,
                ITemperatureStreamRepo temperatureStreamRepo,
                IPowerStreamRepo powerStreamRepo,
                ICadenceStreamRepo cadenceStreamRepo,
                IHeartrateStreamRepo heartrateStreamRepo,
                ISmoothVelocityStreamRepo smoothVelocityStreamRepo,
                IAltitudeStreamRepo altitudeStreamRepo) : IUnitOfWork
{
    public IActivitiesRepo Activities { get; } = activitiesRepo;
    public IAthleteRepo Athletes { get; } = athleteRepo;
    public IMapRepo Maps { get; } = mapRepo;
    public ISegmentRepo Segments { get; } = segmentRepo;
    public ISegmentEffortRepo SegmentEfforts { get; } = segmentEffortRepo;
    public IStreamSetRepo StreamSets { get; } = streamSetRepo;
    public ITimeStreamRepo TimeStreams { get; } = timeStreamRepo;
    public IDistanceStreamRepo DistanceStreams { get; } = distanceStreamRepo;
    public ILatLngStreamRepo LatLngStreams { get; } = latLngStreamRepo;
    public ISmoothGradeStreamRepo SmoothGradeStreams { get; } = smoothGradeStreamRepo;
    public IMovingStreamRepo MovingStreams { get; } = movingStreamRepo;
    public ITemperatureStreamRepo TemperatureStreams { get; } = temperatureStreamRepo;
    public IPowerStreamRepo PowerStreams { get; } = powerStreamRepo;
    public ICadenceStreamRepo CadenceStreams { get; } = cadenceStreamRepo;
    public IHeartrateStreamRepo HeartrateStreams { get; } = heartrateStreamRepo;
    public ISmoothVelocityStreamRepo SmoothVelocityStreams { get; } = smoothVelocityStreamRepo;
    public IAltitudeStreamRepo AltitudeStreams { get; } = altitudeStreamRepo;

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
