using Contracts.Interfaces;
using Data.Models.Strava;
using Newtonsoft.Json;

namespace Data.BusinessLogic
{
    public class DataSaver(IUnitOfWork unitOfWork)
    {
        public async Task SaveActivity(DetailedActivity activity, int athleteId)
        {
            await unitOfWork.Athletes.AddOrEditAthlete(activity.Athlete);

            activity.AthleteId = athleteId;
            activity.Athlete = null;

            DetailedActivity activityCopy = JsonConvert.DeserializeObject<DetailedActivity>(JsonConvert.SerializeObject(activity));
            activityCopy.AthleteId = athleteId;
            activityCopy.Map = null;
            activityCopy.SegmentEfforts = null;
            activityCopy.Laps = null;
            activityCopy.BestEfforts = null;

            await unitOfWork.Activities.AddOrEditActivity(activityCopy);

            if (activity.Map != null)
            {
                activity.Map.ActivityId = activity.Id;
                activity.MapId = await unitOfWork.Maps.AddOrEditMap(activity.Map);
                activity.Map = null;
            }

            if (activity.SegmentEfforts != null)
            {
                foreach (DetailedSegmentEffort segmentEffort in activity.SegmentEfforts)
                {
                    long? segmentId = await unitOfWork.Segments.AddOrEditSegment(segmentEffort.Segment);
                    segmentEffort.SegmentId = segmentId;
                    segmentEffort.Segment = null;

                    segmentEffort.ActivityId = activity.Id;
                    await unitOfWork.SegmentEfforts.AddOrEditSegmentEffortAsync(segmentEffort);
                }
            }

            if (activity.Laps != null)
            {
                foreach (Lap lap in activity.Laps)
                {
                    lap.DetailedActivity = null;
                }
            }

            if (activity.BestEfforts != null)
            {
                foreach (DetailedSegmentEffort bestEffort in activity.BestEfforts)
                {
                    bestEffort.Segment = null;
                    bestEffort.ActivityId = activity.Id;
                    await unitOfWork.SegmentEfforts.AddOrEditSegmentEffortAsync(bestEffort);
                }
            }

            await unitOfWork.Activities.AddOrEditActivity(activity);
        }

        public async Task SaveStreams(StreamSet streamSet, long? activityId)
        {
            StreamSet streamSetCopy = JsonConvert.DeserializeObject<StreamSet>(JsonConvert.SerializeObject(streamSet));

            streamSetCopy.ActivityId = activityId;
            streamSetCopy.Altitude = null;
            streamSetCopy.Distance = null;
            streamSetCopy.Latlng = null;
            streamSetCopy.Time = null;
            streamSetCopy.VelocitySmooth = null;
            streamSetCopy.Heartrate = null;
            streamSetCopy.Cadence = null;
            streamSetCopy.Watts = null;
            streamSetCopy.GradeSmooth = null;
            streamSetCopy.Moving = null;
            streamSetCopy.Temp = null;

            await unitOfWork.StreamSets.AddStreamSetAsync(streamSetCopy);

            if (streamSet.Altitude != null)
            {
                streamSet.Altitude.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.AltitudeStreams.AddAltitudeStreamAsync(streamSet.Altitude);
            }
            if (streamSet.Distance != null)
            {
                streamSet.Distance.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.DistanceStreams.AddDistanceStreamAsync(streamSet.Distance);
            }
            if (streamSet.Latlng != null)
            {
                streamSet.Latlng.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.LatLngStreams.AddLatLngStreamAsync(streamSet.Latlng);
            }
            if (streamSet.Time != null)
            {
                streamSet.Time.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.TimeStreams.AddTimeStreamAsync(streamSet.Time);
            }
            if (streamSet.VelocitySmooth != null)
            {
                streamSet.VelocitySmooth.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.SmoothVelocityStreams.AddSmoothVelocityStreamAsync(streamSet.VelocitySmooth);
            }
            if (streamSet.Heartrate != null)
            {
                streamSet.Heartrate.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.HeartrateStreams.AddHeartrateStreamAsync(streamSet.Heartrate);
            }
            if (streamSet.Cadence != null)
            {
                streamSet.Cadence.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.CadenceStreams.AddCadenceStreamAsync(streamSet.Cadence);
            }
            if (streamSet.Watts != null)
            {
                streamSet.Watts.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.PowerStreams.AddPowerStreamAsync(streamSet.Watts);
            }
            if (streamSet.GradeSmooth != null)
            {
                streamSet.GradeSmooth.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.SmoothGradeStreams.AddSmoothGradeStreamAsync(streamSet.GradeSmooth);
            }
            if (streamSet.Moving != null)
            {
                streamSet.Moving.StreamSetId = streamSetCopy.StreamSetId;
                await unitOfWork.MovingStreams.AddMovingStreamAsync(streamSet.Moving);
            }
        }
    }
}