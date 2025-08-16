using Contracts.Interfaces;
using Data.Context;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class StreamSetRepo(DatabaseContext context) : IStreamSetRepo
    {
        public async Task AddStreamSetAsync(IStreamSet streamSet)
        {
            var streamSetEntity = streamSet as StreamSet;
            await context.Streams.AddAsync(streamSetEntity);
            await context.SaveChangesAsync();
        }

        public async Task<IStreamSet?> GetStreamSetByIdAsync(int id)
        {
            return await context.Streams.FindAsync(id);
        }

        public async Task UpdateStreamSetAsync(IStreamSet streamSet)
        {
            var streamSetEntity = streamSet as StreamSet;
            context.Streams.Update(streamSetEntity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStreamSetAsync(int id)
        {
            var streamSet = await context.Streams.FindAsync(id);
            if (streamSet != null)
            {
                context.Streams.Remove(streamSet);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<long?>> GetAllActivityIdsFromStreamSetsAsync()
        {
            return await context.Streams.Select(s => s.ActivityId).ToListAsync();
        }
    }

    public class TimeStreamRepo : ITimeStreamRepo
    {
        private readonly DatabaseContext _context;

        public TimeStreamRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddTimeStreamAsync(ITimeStream timeStream)
        {
            var timeStreamEntity = timeStream as TimeStream;
            await _context.TimeStreams.AddAsync(timeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ITimeStream?> GetTimeStreamByIdAsync(int id)
        {
            return await _context.TimeStreams.FindAsync(id);
        }

        public async Task UpdateTimeStreamAsync(ITimeStream timeStream)
        {
            var timeStreamEntity = timeStream as TimeStream;
            _context.TimeStreams.Update(timeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTimeStreamAsync(int id)
        {
            var timeStream = await _context.TimeStreams.FindAsync(id);
            if (timeStream != null)
            {
                _context.TimeStreams.Remove(timeStream);
                await _context.SaveChangesAsync();
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

        public async Task AddDistanceStreamAsync(IDistanceStream distanceStream)
        {
            var distanceStreamEntity = distanceStream as DistanceStream;
            await _context.DistanceStreams.AddAsync(distanceStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IDistanceStream?> GetDistanceStreamByIdAsync(int id)
        {
            return await _context.DistanceStreams.FindAsync(id);
        }

        public async Task UpdateDistanceStreamAsync(IDistanceStream distanceStream)
        {
            var distanceStreamEntity = distanceStream as DistanceStream;
            _context.DistanceStreams.Update(distanceStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDistanceStreamAsync(int id)
        {
            var distanceStream = await _context.DistanceStreams.FindAsync(id);
            if (distanceStream != null)
            {
                _context.DistanceStreams.Remove(distanceStream);
                await _context.SaveChangesAsync();
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

        public async Task AddLatLngStreamAsync(ILatLngStream latLngStream)
        {
            var latLngStreamEntity = latLngStream as LatLngStream;
            await _context.LatLngStreams.AddAsync(latLngStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ILatLngStream?> GetLatLngStreamByIdAsync(int id)
        {
            return await _context.LatLngStreams.FindAsync(id);
        }

        public async Task UpdateLatLngStreamAsync(ILatLngStream latLngStream)
        {
            var latLngStreamEntity = latLngStream as LatLngStream;
            _context.LatLngStreams.Update(latLngStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLatLngStreamAsync(int id)
        {
            var latLngStream = await _context.LatLngStreams.FindAsync(id);
            if (latLngStream != null)
            {
                _context.LatLngStreams.Remove(latLngStream);
                await _context.SaveChangesAsync();
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

        public async Task AddSmoothGradeStreamAsync(ISmoothGradeStream smoothGradeStream)
        {
            var smoothGradeStreamEntity = smoothGradeStream as SmoothGradeStream;
            await _context.SmoothGradeStreams.AddAsync(smoothGradeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ISmoothGradeStream?> GetSmoothGradeStreamByIdAsync(int id)
        {
            return await _context.SmoothGradeStreams.FindAsync(id);
        }

        public async Task UpdateSmoothGradeStreamAsync(ISmoothGradeStream smoothGradeStream)
        {
            var smoothGradeStreamEntity = smoothGradeStream as SmoothGradeStream;
            _context.SmoothGradeStreams.Update(smoothGradeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSmoothGradeStreamAsync(int id)
        {
            var smoothGradeStream = await _context.SmoothGradeStreams.FindAsync(id);
            if (smoothGradeStream != null)
            {
                _context.SmoothGradeStreams.Remove(smoothGradeStream);
                await _context.SaveChangesAsync();
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

        public async Task AddMovingStreamAsync(IMovingStream movingStream)
        {
            var movingStreamEntity = movingStream as MovingStream;
            await _context.MovingStreams.AddAsync(movingStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IMovingStream?> GetMovingStreamByIdAsync(int id)
        {
            return await _context.MovingStreams.FindAsync(id);
        }

        public async Task UpdateMovingStreamAsync(IMovingStream movingStream)
        {
            var movingStreamEntity = movingStream as MovingStream;
            _context.MovingStreams.Update(movingStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovingStreamAsync(int id)
        {
            var movingStream = await _context.MovingStreams.FindAsync(id);
            if (movingStream != null)
            {
                _context.MovingStreams.Remove(movingStream);
                await _context.SaveChangesAsync();
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

        public async Task AddTemperatureStreamAsync(ITemperatureStream temperatureStream)
        {
            var temperatureStreamEntity = temperatureStream as TemperatureStream;
            await _context.TemperatureStreams.AddAsync(temperatureStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ITemperatureStream?> GetTemperatureStreamByIdAsync(int id)
        {
            return await _context.TemperatureStreams.FindAsync(id);
        }

        public async Task UpdateTemperatureStreamAsync(ITemperatureStream temperatureStream)
        {
            var temperatureStreamEntity = temperatureStream as TemperatureStream;
            _context.TemperatureStreams.Update(temperatureStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTemperatureStreamAsync(int id)
        {
            var temperatureStream = await _context.TemperatureStreams.FindAsync(id);
            if (temperatureStream != null)
            {
                _context.TemperatureStreams.Remove(temperatureStream);
                await _context.SaveChangesAsync();
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

        public async Task AddPowerStreamAsync(IPowerStream powerStream)
        {
            var powerStreamEntity = powerStream as PowerStream;
            await _context.PowerStreams.AddAsync(powerStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IPowerStream?> GetPowerStreamByIdAsync(int id)
        {
            return await _context.PowerStreams.FindAsync(id);
        }

        public async Task UpdatePowerStreamAsync(IPowerStream powerStream)
        {
            var powerStreamEntity = powerStream as PowerStream;
            _context.PowerStreams.Update(powerStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePowerStreamAsync(int id)
        {
            var powerStream = await _context.PowerStreams.FindAsync(id);
            if (powerStream != null)
            {
                _context.PowerStreams.Remove(powerStream);
                await _context.SaveChangesAsync();
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

        public async Task AddCadenceStreamAsync(ICadenceStream cadenceStream)
        {
            var cadenceStreamEntity = cadenceStream as CadenceStream;
            await _context.CadenceStreams.AddAsync(cadenceStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICadenceStream?> GetCadenceStreamByIdAsync(int id)
        {
            return await _context.CadenceStreams.FindAsync(id);
        }

        public async Task UpdateCadenceStreamAsync(ICadenceStream cadenceStream)
        {
            var cadenceStreamEntity = cadenceStream as CadenceStream;
            _context.CadenceStreams.Update(cadenceStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCadenceStreamAsync(int id)
        {
            var cadenceStream = await _context.CadenceStreams.FindAsync(id);
            if (cadenceStream != null)
            {
                _context.CadenceStreams.Remove(cadenceStream);
                await _context.SaveChangesAsync();
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

        public async Task AddHeartrateStreamAsync(IHeartrateStream heartrateStream)
        {
            var heartrateStreamEntity = heartrateStream as HeartrateStream;
            await _context.HeartrateStreams.AddAsync(heartrateStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IHeartrateStream?> GetHeartrateStreamByIdAsync(int id)
        {
            return await _context.HeartrateStreams.FindAsync(id);
        }

        public async Task UpdateHeartrateStreamAsync(IHeartrateStream heartrateStream)
        {
            var heartrateStreamEntity = heartrateStream as HeartrateStream;
            _context.HeartrateStreams.Update(heartrateStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHeartrateStreamAsync(int id)
        {
            var heartrateStream = await _context.HeartrateStreams.FindAsync(id);
            if (heartrateStream != null)
            {
                _context.HeartrateStreams.Remove(heartrateStream);
                await _context.SaveChangesAsync();
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

        public async Task AddSmoothVelocityStreamAsync(ISmoothVelocityStream smoothVelocityStream)
        {
            var smoothVelocityStreamEntity = smoothVelocityStream as SmoothVelocityStream;
            await _context.SmoothVelocityStreams.AddAsync(smoothVelocityStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<ISmoothVelocityStream?> GetSmoothVelocityStreamByIdAsync(int id)
        {
            return await _context.SmoothVelocityStreams.FindAsync(id);
        }

        public async Task UpdateSmoothVelocityStreamAsync(ISmoothVelocityStream smoothVelocityStream)
        {
            var smoothVelocityStreamEntity = smoothVelocityStream as SmoothVelocityStream;
            _context.SmoothVelocityStreams.Update(smoothVelocityStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSmoothVelocityStreamAsync(int id)
        {
            var smoothVelocityStream = await _context.SmoothVelocityStreams.FindAsync(id);
            if (smoothVelocityStream != null)
            {
                _context.SmoothVelocityStreams.Remove(smoothVelocityStream);
                await _context.SaveChangesAsync();
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

        public async Task AddAltitudeStreamAsync(IAltitudeStream altitudeStream)
        {
            var altitudeStreamEntity = altitudeStream as AltitudeStream;
            await _context.AltitudeStreams.AddAsync(altitudeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IAltitudeStream?> GetAltitudeStreamByIdAsync(int id)
        {
            return await _context.AltitudeStreams.FindAsync(id);
        }

        public async Task UpdateAltitudeStreamAsync(IAltitudeStream altitudeStream)
        {
            var altitudeStreamEntity = altitudeStream as AltitudeStream;
            _context.AltitudeStreams.Update(altitudeStreamEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAltitudeStreamAsync(int id)
        {
            var altitudeStream = await _context.AltitudeStreams.FindAsync(id);
            if (altitudeStream != null)
            {
                _context.AltitudeStreams.Remove(altitudeStream);
                await _context.SaveChangesAsync();
            }
        }
    }
}