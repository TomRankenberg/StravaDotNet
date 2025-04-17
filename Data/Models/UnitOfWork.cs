using Data.Context;
using Data.Interfaces;
using Data.Repos;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context, 
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
                    IAltitudeStreamRepo altitudeStreamRepo)
    {
        _context = context;
        Activities = activitiesRepo;
        Athletes = athleteRepo;
        Maps = mapRepo;
        Segments = segmentRepo;
        SegmentEfforts = segmentEffortRepo;
        StreamSets = streamSetRepo;
        TimeStreams = timeStreamRepo;
        DistanceStreams = distanceStreamRepo;
        LatLngStreams = latLngStreamRepo;
        SmoothGradeStreams = smoothGradeStreamRepo;
        MovingStreams = movingStreamRepo;
        TemperatureStreams = temperatureStreamRepo;
        PowerStreams = powerStreamRepo;
        CadenceStreams = cadenceStreamRepo;
        HeartrateStreams = heartrateStreamRepo;
        SmoothVelocityStreams = smoothVelocityStreamRepo;
        AltitudeStreams = altitudeStreamRepo;

    }

    public IActivitiesRepo Activities { get; }
    public IAthleteRepo Athletes { get; }
    public IMapRepo Maps { get; }
    public ISegmentRepo Segments { get; }
    public ISegmentEffortRepo SegmentEfforts { get; }
    public IStreamSetRepo StreamSets { get; }
    public ITimeStreamRepo TimeStreams { get; }
    public IDistanceStreamRepo DistanceStreams { get; }
    public ILatLngStreamRepo LatLngStreams { get; }
    public ISmoothGradeStreamRepo SmoothGradeStreams { get; }
    public IMovingStreamRepo MovingStreams { get; }
    public ITemperatureStreamRepo TemperatureStreams { get; }
    public IPowerStreamRepo PowerStreams { get; }
    public ICadenceStreamRepo CadenceStreams { get; }
    public IHeartrateStreamRepo HeartrateStreams { get; }
    public ISmoothVelocityStreamRepo SmoothVelocityStreams { get; }
    public IAltitudeStreamRepo AltitudeStreams { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
