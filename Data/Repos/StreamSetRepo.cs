using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;

namespace Data.Repos
{
    public class StreamSetRepo : IStreamSetRepo
    {
        private readonly DatabaseContext _context;

        public StreamSetRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddStreamSet(IStreamSet streamSet)
        {
            var streamSetEntity = streamSet as StreamSet;
            _context.Streams.Add(streamSetEntity);
            _context.SaveChanges();
        }

        public IStreamSet GetStreamSetById(int id)
        {
            return _context.Streams.Find(id);
        }

        public void UpdateStreamSet(IStreamSet streamSet)
        {
            var streamSetEntity = streamSet as StreamSet;
            _context.Streams.Update(streamSetEntity);
            _context.SaveChanges();
        }

        public void DeleteStreamSet(int id)
        {
            var streamSet = _context.Streams.Find(id);
            if (streamSet != null)
            {
                _context.Streams.Remove(streamSet);
                _context.SaveChanges();
            }
        }

        public List<long?> GetAllActivityIdsFromStreamSets()
        {
            return _context.Streams.Select(s => s.ActivityId).ToList();
        }
    }

    public class TimeStreamRepo : ITimeStreamRepo
    {
        private readonly DatabaseContext _context;

        public TimeStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddTimeStream(ITimeStream timeStream)
        {
            var timeStreamEntity = timeStream as TimeStream;
            _context.TimeStreams.Add(timeStreamEntity);
            _context.SaveChanges();
        }

        public ITimeStream GetTimeStreamById(int id)
        {
            return _context.TimeStreams.Find(id);
        }

        public void UpdateTimeStream(ITimeStream timeStream)
        {
            var timeStreamEntity = timeStream as TimeStream;
            _context.TimeStreams.Update(timeStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteTimeStream(int id)
        {
            var timeStream = _context.TimeStreams.Find(id);
            if (timeStream != null)
            {
                _context.TimeStreams.Remove(timeStream);
                _context.SaveChanges();
            }
        }
    }

    public class DistanceStreamRepo : IDistanceStreamRepo
    {
        private readonly DatabaseContext _context;

        public DistanceStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddDistanceStream(IDistanceStream distanceStream)
        {
            var distanceStreamEntity = distanceStream as DistanceStream;
            _context.DistanceStreams.Add(distanceStreamEntity);
            _context.SaveChanges();
        }

        public IDistanceStream GetDistanceStreamById(int id)
        {
            return _context.DistanceStreams.Find(id);
        }

        public void UpdateDistanceStream(IDistanceStream distanceStream)
        {
            var distanceStreamEntity = distanceStream as DistanceStream;
            _context.DistanceStreams.Update(distanceStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteDistanceStream(int id)
        {
            var distanceStream = _context.DistanceStreams.Find(id);
            if (distanceStream != null)
            {
                _context.DistanceStreams.Remove(distanceStream);
                _context.SaveChanges();
            }
        }
    }
    public class LatLngStreamRepo : ILatLngStreamRepo
    {
        private readonly DatabaseContext _context;

        public LatLngStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddLatLngStream(ILatLngStream latLngStream)
        {
            var latLngStreamEntity = latLngStream as LatLngStream;
            _context.LatLngStreams.Add(latLngStreamEntity);
            _context.SaveChanges();
        }

        public ILatLngStream GetLatLngStreamById(int id)
        {
            return _context.LatLngStreams.Find(id);
        }

        public void UpdateLatLngStream(ILatLngStream latLngStream)
        {
            var latLngStreamEntity = latLngStream as LatLngStream;
            _context.LatLngStreams.Update(latLngStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteLatLngStream(int id)
        {
            var latLngStream = _context.LatLngStreams.Find(id);
            if (latLngStream != null)
            {
                _context.LatLngStreams.Remove(latLngStream);
                _context.SaveChanges();
            }
        }
    }

    public class SmoothGradeStreamRepo : ISmoothGradeStreamRepo
    {
        private readonly DatabaseContext _context;

        public SmoothGradeStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddSmoothGradeStream(ISmoothGradeStream smoothGradeStream)
        {
            var smoothGradeStreamEntity = smoothGradeStream as SmoothGradeStream;
            _context.SmoothGradeStreams.Add(smoothGradeStreamEntity);
            _context.SaveChanges();
        }

        public ISmoothGradeStream GetSmoothGradeStreamById(int id)
        {
            return _context.SmoothGradeStreams.Find(id);
        }

        public void UpdateSmoothGradeStream(ISmoothGradeStream smoothGradeStream)
        {
            var smoothGradeStreamEntity = smoothGradeStream as SmoothGradeStream;
            _context.SmoothGradeStreams.Update(smoothGradeStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteSmoothGradeStream(int id)
        {
            var smoothGradeStream = _context.SmoothGradeStreams.Find(id);
            if (smoothGradeStream != null)
            {
                _context.SmoothGradeStreams.Remove(smoothGradeStream);
                _context.SaveChanges();
            }
        }
    }

    public class MovingStreamRepo : IMovingStreamRepo
    {
        private readonly DatabaseContext _context;

        public MovingStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddMovingStream(IMovingStream movingStream)
        {
            var movingStreamEntity = movingStream as MovingStream;
            _context.MovingStreams.Add(movingStreamEntity);
            _context.SaveChanges();
        }

        public IMovingStream GetMovingStreamById(int id)
        {
            return _context.MovingStreams.Find(id);
        }

        public void UpdateMovingStream(IMovingStream movingStream)
        {
            var movingStreamEntity = movingStream as MovingStream;
            _context.MovingStreams.Update(movingStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteMovingStream(int id)
        {
            var movingStream = _context.MovingStreams.Find(id);
            if (movingStream != null)
            {
                _context.MovingStreams.Remove(movingStream);
                _context.SaveChanges();
            }
        }
    }

    public class TemperatureStreamRepo : ITemperatureStreamRepo
    {
        private readonly DatabaseContext _context;

        public TemperatureStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddTemperatureStream(ITemperatureStream temperatureStream)
        {
            var temperatureStreamEntity = temperatureStream as TemperatureStream;
            _context.TemperatureStreams.Add(temperatureStreamEntity);
            _context.SaveChanges();
        }

        public ITemperatureStream GetTemperatureStreamById(int id)
        {
            return _context.TemperatureStreams.Find(id);
        }

        public void UpdateTemperatureStream(ITemperatureStream temperatureStream)
        {
            var temperatureStreamEntity = temperatureStream as TemperatureStream;
            _context.TemperatureStreams.Update(temperatureStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteTemperatureStream(int id)
        {
            var temperatureStream = _context.TemperatureStreams.Find(id);
            if (temperatureStream != null)
            {
                _context.TemperatureStreams.Remove(temperatureStream);
                _context.SaveChanges();
            }
        }
    }

    public class PowerStreamRepo : IPowerStreamRepo
    {
        private readonly DatabaseContext _context;

        public PowerStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddPowerStream(IPowerStream powerStream)
        {
            var powerStreamEntity = powerStream as PowerStream;
            _context.PowerStreams.Add(powerStreamEntity);
            _context.SaveChanges();
        }

        public IPowerStream GetPowerStreamById(int id)
        {
            return _context.PowerStreams.Find(id);
        }

        public void UpdatePowerStream(IPowerStream powerStream)
        {
            var powerStreamEntity = powerStream as PowerStream;
            _context.PowerStreams.Update(powerStreamEntity);
            _context.SaveChanges();
        }

        public void DeletePowerStream(int id)
        {
            var powerStream = _context.PowerStreams.Find(id);
            if (powerStream != null)
            {
                _context.PowerStreams.Remove(powerStream);
                _context.SaveChanges();
            }
        }
    }

    public class CadenceStreamRepo : ICadenceStreamRepo
    {
        private readonly DatabaseContext _context;

        public CadenceStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddCadenceStream(ICadenceStream cadenceStream)
        {
            var cadenceStreamEntity = cadenceStream as CadenceStream;
            _context.CadenceStreams.Add(cadenceStreamEntity);
            _context.SaveChanges();
        }

        public ICadenceStream GetCadenceStreamById(int id)
        {
            return _context.CadenceStreams.Find(id);
        }

        public void UpdateCadenceStream(ICadenceStream cadenceStream)
        {
            var cadenceStreamEntity = cadenceStream as CadenceStream;
            _context.CadenceStreams.Update(cadenceStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteCadenceStream(int id)
        {
            var cadenceStream = _context.CadenceStreams.Find(id);
            if (cadenceStream != null)
            {
                _context.CadenceStreams.Remove(cadenceStream);
                _context.SaveChanges();
            }
        }
    }

    public class HeartrateStreamRepo : IHeartrateStreamRepo
    {
        private readonly DatabaseContext _context;

        public HeartrateStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddHeartrateStream(IHeartrateStream heartrateStream)
        {
            var heartrateStreamEntity = heartrateStream as HeartrateStream;
            _context.HeartrateStreams.Add(heartrateStreamEntity);
            _context.SaveChanges();
        }

        public IHeartrateStream GetHeartrateStreamById(int id)
        {
            return _context.HeartrateStreams.Find(id);
        }

        public void UpdateHeartrateStream(IHeartrateStream heartrateStream)
        {
            var heartrateStreamEntity = heartrateStream as HeartrateStream;
            _context.HeartrateStreams.Update(heartrateStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteHeartrateStream(int id)
        {
            var heartrateStream = _context.HeartrateStreams.Find(id);
            if (heartrateStream != null)
            {
                _context.HeartrateStreams.Remove(heartrateStream);
                _context.SaveChanges();
            }
        }
    }

    public class SmoothVelocityStreamRepo : ISmoothVelocityStreamRepo
    {
        private readonly DatabaseContext _context;

        public SmoothVelocityStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddSmoothVelocityStream(ISmoothVelocityStream smoothVelocityStream)
        {
            var smoothVelocityStreamEntity = smoothVelocityStream as SmoothVelocityStream;
            _context.SmoothVelocityStreams.Add(smoothVelocityStreamEntity);
            _context.SaveChanges();
        }

        public ISmoothVelocityStream GetSmoothVelocityStreamById(int id)
        {
            return _context.SmoothVelocityStreams.Find(id);
        }

        public void UpdateSmoothVelocityStream(ISmoothVelocityStream smoothVelocityStream)
        {
            var smoothVelocityStreamEntity = smoothVelocityStream as SmoothVelocityStream;
            _context.SmoothVelocityStreams.Update(smoothVelocityStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteSmoothVelocityStream(int id)
        {
            var smoothVelocityStream = _context.SmoothVelocityStreams.Find(id);
            if (smoothVelocityStream != null)
            {
                _context.SmoothVelocityStreams.Remove(smoothVelocityStream);
                _context.SaveChanges();
            }
        }
    }

    public class AltitudeStreamRepo : IAltitudeStreamRepo
    {
        private readonly DatabaseContext _context;

        public AltitudeStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public void AddAltitudeStream(IAltitudeStream altitudeStream)
        {
            var altitudeStreamEntity = altitudeStream as AltitudeStream;
            _context.AltitudeStreams.Add(altitudeStreamEntity);
            _context.SaveChanges();
        }

        public IAltitudeStream GetAltitudeStreamById(int id)
        {
            return _context.AltitudeStreams.Find(id);
        }

        public void UpdateAltitudeStream(IAltitudeStream altitudeStream)
        {
            var altitudeStreamEntity = altitudeStream as AltitudeStream;
            _context.AltitudeStreams.Update(altitudeStreamEntity);
            _context.SaveChanges();
        }

        public void DeleteAltitudeStream(int id)
        {
            var altitudeStream = _context.AltitudeStreams.Find(id);
            if (altitudeStream != null)
            {
                _context.AltitudeStreams.Remove(altitudeStream);
                _context.SaveChanges();
            }
        }
    }
    // Repeat similar implementations for DistanceStreamRepo, LatLngStreamRepo, SmoothGradeStreamRepo, etc.
}