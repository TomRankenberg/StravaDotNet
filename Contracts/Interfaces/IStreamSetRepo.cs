namespace Contracts.Interfaces
{
    public interface IStreamSetRepo
    {
        void AddStreamSet(IStreamSet streamSet);

        IStreamSet GetStreamSetById(int id);

        void UpdateStreamSet(IStreamSet streamSet);

        void DeleteStreamSet(int id);
        List<long?> GetAllActivityIdsFromStreamSets();

    }

    public interface ITimeStreamRepo
    {
        void AddTimeStream(ITimeStream timeStream);

        ITimeStream GetTimeStreamById(int id);

        void UpdateTimeStream(ITimeStream timeStream);

        void DeleteTimeStream(int id);
    }

    public interface IDistanceStreamRepo
    {
        void AddDistanceStream(IDistanceStream distanceStream);

        IDistanceStream GetDistanceStreamById(int id);

        void UpdateDistanceStream(IDistanceStream distanceStream);

        void DeleteDistanceStream(int id);
    }

    public interface ILatLngStreamRepo
    {
        void AddLatLngStream(ILatLngStream latLngStream);

        ILatLngStream GetLatLngStreamById(int id);

        void UpdateLatLngStream(ILatLngStream latLngStream);

        void DeleteLatLngStream(int id);
    }

    public interface ISmoothGradeStreamRepo
    {
        void AddSmoothGradeStream(ISmoothGradeStream smoothGradeStream);

        ISmoothGradeStream GetSmoothGradeStreamById(int id);

        void UpdateSmoothGradeStream(ISmoothGradeStream smoothGradeStream);

        void DeleteSmoothGradeStream(int id);
    }

    public interface IMovingStreamRepo
    {
        void AddMovingStream(IMovingStream movingStream);

        IMovingStream GetMovingStreamById(int id);

        void UpdateMovingStream(IMovingStream movingStream);

        void DeleteMovingStream(int id);
    }

    public interface ITemperatureStreamRepo
    {
        void AddTemperatureStream(ITemperatureStream temperatureStream);

        ITemperatureStream GetTemperatureStreamById(int id);

        void UpdateTemperatureStream(ITemperatureStream temperatureStream);

        void DeleteTemperatureStream(int id);
    }

    public interface IPowerStreamRepo
    {
        void AddPowerStream(IPowerStream powerStream);

        IPowerStream GetPowerStreamById(int id);

        void UpdatePowerStream(IPowerStream powerStream);

        void DeletePowerStream(int id);
    }

    public interface ICadenceStreamRepo
    {
        void AddCadenceStream(ICadenceStream cadenceStream);

        ICadenceStream GetCadenceStreamById(int id);

        void UpdateCadenceStream(ICadenceStream cadenceStream);

        void DeleteCadenceStream(int id);
    }

    public interface IHeartrateStreamRepo
    {
        void AddHeartrateStream(IHeartrateStream heartrateStream);

        IHeartrateStream GetHeartrateStreamById(int id);

        void UpdateHeartrateStream(IHeartrateStream heartrateStream);

        void DeleteHeartrateStream(int id);
    }

    public interface ISmoothVelocityStreamRepo
    {
        void AddSmoothVelocityStream(ISmoothVelocityStream smoothVelocityStream);

        ISmoothVelocityStream GetSmoothVelocityStreamById(int id);

        void UpdateSmoothVelocityStream(ISmoothVelocityStream smoothVelocityStream);

        void DeleteSmoothVelocityStream(int id);
    }

    public interface IAltitudeStreamRepo
    {
        void AddAltitudeStream(IAltitudeStream altitudeStream);

        IAltitudeStream GetAltitudeStreamById(int id);

        void UpdateAltitudeStream(IAltitudeStream altitudeStream);

        void DeleteAltitudeStream(int id);
    }
}