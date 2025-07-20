namespace Contracts.Interfaces
{
    public interface IStreamSet
    {
        long? ActivityId { get; set; }
        IAltitudeStream Altitude { get; set; }
        ICadenceStream Cadence { get; set; }
        IDistanceStream Distance { get; set; }
        ISmoothGradeStream GradeSmooth { get; set; }
        IHeartrateStream Heartrate { get; set; }
        ILatLngStream Latlng { get; set; }
        IMovingStream Moving { get; set; }
        int? StreamSetId { get; set; }
        ITemperatureStream Temp { get; set; }
        ITimeStream Time { get; set; }
        ISmoothVelocityStream VelocitySmooth { get; set; }
        IPowerStream Watts { get; set; }

        string ToJson();
        string ToString();
    }
}