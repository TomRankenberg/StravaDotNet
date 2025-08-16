namespace Contracts.Interfaces
{
    public interface IStreamSetRepo
    {
        Task AddStreamSetAsync(IStreamSet streamSet);

        Task<IStreamSet?> GetStreamSetByIdAsync(int id);

        Task UpdateStreamSetAsync(IStreamSet streamSet);

        Task DeleteStreamSetAsync(int id);
        Task<List<long?>> GetAllActivityIdsFromStreamSetsAsync();

    }

    public interface ITimeStreamRepo
    {
        Task AddTimeStreamAsync(ITimeStream timeStream);

        Task<ITimeStream> GetTimeStreamByIdAsync(int id);

        Task UpdateTimeStreamAsync(ITimeStream timeStream);

        Task DeleteTimeStreamAsync(int id);
    }

    public interface IDistanceStreamRepo
    {
        Task AddDistanceStreamAsync(IDistanceStream distanceStream);

        Task<IDistanceStream> GetDistanceStreamByIdAsync(int id);

        Task UpdateDistanceStreamAsync(IDistanceStream distanceStream);

        Task DeleteDistanceStreamAsync(int id);
    }

    public interface ILatLngStreamRepo
    {
        Task AddLatLngStreamAsync(ILatLngStream latLngStream);

        Task<ILatLngStream> GetLatLngStreamByIdAsync(int id);

        Task UpdateLatLngStreamAsync(ILatLngStream latLngStream);

        Task DeleteLatLngStreamAsync(int id);
    }

    public interface ISmoothGradeStreamRepo
    {
        Task AddSmoothGradeStreamAsync(ISmoothGradeStream smoothGradeStream);

        Task<ISmoothGradeStream?> GetSmoothGradeStreamByIdAsync(int id);

        Task UpdateSmoothGradeStreamAsync(ISmoothGradeStream smoothGradeStream);

        Task DeleteSmoothGradeStreamAsync(int id);
    }

    public interface IMovingStreamRepo
    {
        Task AddMovingStreamAsync(IMovingStream movingStream);

        Task<IMovingStream?> GetMovingStreamByIdAsync(int id);

        Task UpdateMovingStreamAsync(IMovingStream movingStream);

        Task DeleteMovingStreamAsync(int id);
    }

    public interface ITemperatureStreamRepo
    {
        Task AddTemperatureStreamAsync(ITemperatureStream temperatureStream);

        Task<ITemperatureStream?> GetTemperatureStreamByIdAsync(int id);

        Task UpdateTemperatureStreamAsync(ITemperatureStream temperatureStream);

        Task DeleteTemperatureStreamAsync(int id);
    }

    public interface IPowerStreamRepo
    {
        Task AddPowerStreamAsync(IPowerStream powerStream);

        Task<IPowerStream?> GetPowerStreamByIdAsync(int id);

        Task UpdatePowerStreamAsync(IPowerStream powerStream);

        Task DeletePowerStreamAsync(int id);
    }

    public interface ICadenceStreamRepo
    {
        Task AddCadenceStreamAsync(ICadenceStream cadenceStream);

        Task<ICadenceStream?> GetCadenceStreamByIdAsync(int id);

        Task UpdateCadenceStreamAsync(ICadenceStream cadenceStream);

        Task DeleteCadenceStreamAsync(int id);
    }

    public interface IHeartrateStreamRepo
    {
        Task AddHeartrateStreamAsync(IHeartrateStream heartrateStream);

        Task<IHeartrateStream?> GetHeartrateStreamByIdAsync(int id);

        Task UpdateHeartrateStreamAsync(IHeartrateStream heartrateStream);

        Task DeleteHeartrateStreamAsync(int id);
    }

    public interface ISmoothVelocityStreamRepo
    {
        Task AddSmoothVelocityStreamAsync(ISmoothVelocityStream smoothVelocityStream);

        Task<ISmoothVelocityStream?> GetSmoothVelocityStreamByIdAsync(int id);

        Task UpdateSmoothVelocityStreamAsync(ISmoothVelocityStream smoothVelocityStream);

        Task DeleteSmoothVelocityStreamAsync(int id);
    }

    public interface IAltitudeStreamRepo
    {
        Task AddAltitudeStreamAsync(IAltitudeStream altitudeStream);

        Task<IAltitudeStream?> GetAltitudeStreamByIdAsync(int id);

        Task UpdateAltitudeStreamAsync(IAltitudeStream altitudeStream);

        Task DeleteAltitudeStreamAsync(int id);
    }
}