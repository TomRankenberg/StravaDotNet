using Data.Models.Strava;

namespace Data.Interfaces
{
    public interface IStreamSetRepo
    {
        void AddStreamSet(StreamSet streamSet);

        StreamSet GetStreamSetById(int id);

        void UpdateStreamSet(StreamSet streamSet);

        void DeleteStreamSet(int id);
        List<long?> GetAllActivityIdsFromStreamSets();

    }

    public interface ITimeStreamRepo
    {
        void AddTimeStream(TimeStream timeStream);

        TimeStream GetTimeStreamById(int id);

        void UpdateTimeStream(TimeStream timeStream);

        void DeleteTimeStream(int id);
    }

    public interface IDistanceStreamRepo
    {
        void AddDistanceStream(DistanceStream distanceStream);

        DistanceStream GetDistanceStreamById(int id);

        void UpdateDistanceStream(DistanceStream distanceStream);

        void DeleteDistanceStream(int id);
    }

    public interface ILatLngStreamRepo
    {
        void AddLatLngStream(LatLngStream latLngStream);

        LatLngStream GetLatLngStreamById(int id);

        void UpdateLatLngStream(LatLngStream latLngStream);

        void DeleteLatLngStream(int id);
    }

    public interface ISmoothGradeStreamRepo
    {
        void AddSmoothGradeStream(SmoothGradeStream smoothGradeStream);

        SmoothGradeStream GetSmoothGradeStreamById(int id);

        void UpdateSmoothGradeStream(SmoothGradeStream smoothGradeStream);

        void DeleteSmoothGradeStream(int id);
    }

    public interface IMovingStreamRepo
    {
        void AddMovingStream(MovingStream movingStream);

        MovingStream GetMovingStreamById(int id);

        void UpdateMovingStream(MovingStream movingStream);

        void DeleteMovingStream(int id);
    }

    public interface ITemperatureStreamRepo
    {
        void AddTemperatureStream(TemperatureStream temperatureStream);

        TemperatureStream GetTemperatureStreamById(int id);

        void UpdateTemperatureStream(TemperatureStream temperatureStream);

        void DeleteTemperatureStream(int id);
    }

    public interface IPowerStreamRepo
    {
        void AddPowerStream(PowerStream powerStream);

        PowerStream GetPowerStreamById(int id);

        void UpdatePowerStream(PowerStream powerStream);

        void DeletePowerStream(int id);
    }

    public interface ICadenceStreamRepo
    {
        void AddCadenceStream(CadenceStream cadenceStream);

        CadenceStream GetCadenceStreamById(int id);

        void UpdateCadenceStream(CadenceStream cadenceStream);

        void DeleteCadenceStream(int id);
    }

    public interface IHeartrateStreamRepo
    {
        void AddHeartrateStream(HeartrateStream heartrateStream);

        HeartrateStream GetHeartrateStreamById(int id);

        void UpdateHeartrateStream(HeartrateStream heartrateStream);

        void DeleteHeartrateStream(int id);
    }

    public interface ISmoothVelocityStreamRepo
    {
        void AddSmoothVelocityStream(SmoothVelocityStream smoothVelocityStream);

        SmoothVelocityStream GetSmoothVelocityStreamById(int id);

        void UpdateSmoothVelocityStream(SmoothVelocityStream smoothVelocityStream);

        void DeleteSmoothVelocityStream(int id);
    }

    public interface IAltitudeStreamRepo
    {
        void AddAltitudeStream(AltitudeStream altitudeStream);

        AltitudeStream GetAltitudeStreamById(int id);

        void UpdateAltitudeStream(AltitudeStream altitudeStream);

        void DeleteAltitudeStream(int id);
    }
}