using Data.Context;
using Data.Interfaces;
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

        public void AddStreamSet(StreamSet streamSet)
        {
            _context.Streams.Add(streamSet);
            _context.SaveChanges();
        }

        public StreamSet GetStreamSetById(int id)
        {
            return _context.Streams.Find(id);
        }

        public void UpdateStreamSet(StreamSet streamSet)
        {
            _context.Streams.Update(streamSet);
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

        public void AddTimeStream(TimeStream timeStream)
        {
            _context.TimeStreams.Add(timeStream);
            _context.SaveChanges();
        }

        public TimeStream GetTimeStreamById(int id)
        {
            return _context.TimeStreams.Find(id);
        }

        public void UpdateTimeStream(TimeStream timeStream)
        {
            _context.TimeStreams.Update(timeStream);
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

        public void AddDistanceStream(DistanceStream distanceStream)
        {
            _context.DistanceStreams.Add(distanceStream);
            _context.SaveChanges();
        }

        public DistanceStream GetDistanceStreamById(int id)
        {
            return _context.DistanceStreams.Find(id);
        }

        public void UpdateDistanceStream(DistanceStream distanceStream)
        {
            _context.DistanceStreams.Update(distanceStream);
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

        public void AddLatLngStream(LatLngStream latLngStream)
        {
            _context.LatLngStreams.Add(latLngStream);
            _context.SaveChanges();
        }

        public LatLngStream GetLatLngStreamById(int id)
        {
            return _context.LatLngStreams.Find(id);
        }

        public void UpdateLatLngStream(LatLngStream latLngStream)
        {
            _context.LatLngStreams.Update(latLngStream);
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

        public void AddSmoothGradeStream(SmoothGradeStream smoothGradeStream)
        {
            _context.SmoothGradeStreams.Add(smoothGradeStream);
            _context.SaveChanges();
        }

        public SmoothGradeStream GetSmoothGradeStreamById(int id)
        {
            return _context.SmoothGradeStreams.Find(id);
        }

        public void UpdateSmoothGradeStream(SmoothGradeStream smoothGradeStream)
        {
            _context.SmoothGradeStreams.Update(smoothGradeStream);
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

        public void AddMovingStream(MovingStream movingStream)
        {
            _context.MovingStreams.Add(movingStream);
            _context.SaveChanges();
        }

        public MovingStream GetMovingStreamById(int id)
        {
            return _context.MovingStreams.Find(id);
        }

        public void UpdateMovingStream(MovingStream movingStream)
        {
            _context.MovingStreams.Update(movingStream);
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

        public void AddTemperatureStream(TemperatureStream temperatureStream)
        {
            _context.TemperatureStreams.Add(temperatureStream);
            _context.SaveChanges();
        }

        public TemperatureStream GetTemperatureStreamById(int id)
        {
            return _context.TemperatureStreams.Find(id);
        }

        public void UpdateTemperatureStream(TemperatureStream temperatureStream)
        {
            _context.TemperatureStreams.Update(temperatureStream);
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

        public void AddPowerStream(PowerStream powerStream)
        {
            _context.PowerStreams.Add(powerStream);
            _context.SaveChanges();
        }

        public PowerStream GetPowerStreamById(int id)
        {
            return _context.PowerStreams.Find(id);
        }

        public void UpdatePowerStream(PowerStream powerStream)
        {
            _context.PowerStreams.Update(powerStream);
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

        public void AddCadenceStream(CadenceStream cadenceStream)
        {
            _context.CadenceStreams.Add(cadenceStream);
            _context.SaveChanges();
        }

        public CadenceStream GetCadenceStreamById(int id)
        {
            return _context.CadenceStreams.Find(id);
        }

        public void UpdateCadenceStream(CadenceStream cadenceStream)
        {
            _context.CadenceStreams.Update(cadenceStream);
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

        public void AddHeartrateStream(HeartrateStream heartrateStream)
        {
            _context.HeartrateStreams.Add(heartrateStream);
            _context.SaveChanges();
        }

        public HeartrateStream GetHeartrateStreamById(int id)
        {
            return _context.HeartrateStreams.Find(id);
        }

        public void UpdateHeartrateStream(HeartrateStream heartrateStream)
        {
            _context.HeartrateStreams.Update(heartrateStream);
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

        public void AddSmoothVelocityStream(SmoothVelocityStream smoothVelocityStream)
        {
            _context.SmoothVelocityStreams.Add(smoothVelocityStream);
            _context.SaveChanges();
        }

        public SmoothVelocityStream GetSmoothVelocityStreamById(int id)
        {
            return _context.SmoothVelocityStreams.Find(id);
        }

        public void UpdateSmoothVelocityStream(SmoothVelocityStream smoothVelocityStream)
        {
            _context.SmoothVelocityStreams.Update(smoothVelocityStream);
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

        public void AddAltitudeStream(AltitudeStream altitudeStream)
        {
            _context.AltitudeStreams.Add(altitudeStream);
            _context.SaveChanges();
        }

        public AltitudeStream GetAltitudeStreamById(int id)
        {
            return _context.AltitudeStreams.Find(id);
        }

        public void UpdateAltitudeStream(AltitudeStream altitudeStream)
        {
            _context.AltitudeStreams.Update(altitudeStream);
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