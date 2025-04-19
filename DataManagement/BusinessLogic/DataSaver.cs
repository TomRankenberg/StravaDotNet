using Data.Interfaces;
using Data.Models.Strava;
using Newtonsoft.Json;

namespace DataManagement.BusinessLogic
{
    public class DataSaver(IUnitOfWork unitOfWork)
    {
        public void SaveActivity(DetailedActivity activity, int athleteId)
        {
            unitOfWork.Athletes.AddOrEditAthlete(activity.Athlete);

            activity.AthleteId = athleteId;
            activity.Athlete = null;

            DetailedActivity activityCopy = JsonConvert.DeserializeObject<DetailedActivity>(JsonConvert.SerializeObject(activity));
            activityCopy.AthleteId = athleteId;
            activityCopy.Map = null;
            activityCopy.SegmentEfforts = null;
            activityCopy.Laps = null;
            activityCopy.BestEfforts = null;

            unitOfWork.Activities.AddOrEditActivity(activityCopy);

            if (activity.Map != null)
            {
                activity.Map.ActivityId = activity.Id;
                activity.MapId = unitOfWork.Maps.AddOrEditMap(activity.Map);
                activity.Map = null;
            }

            if (activity.SegmentEfforts != null)
            {
                foreach (DetailedSegmentEffort segmentEffort in activity.SegmentEfforts)
                {
                    long? segmentId = unitOfWork.Segments.AddOrEditSegment(segmentEffort.Segment);
                    segmentEffort.SegmentId = segmentId;
                    segmentEffort.Segment = null;

                    segmentEffort.ActivityId = activity.Id;
                    unitOfWork.SegmentEfforts.AddOrEditSegmentEffort(segmentEffort);
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
                    unitOfWork.SegmentEfforts.AddOrEditSegmentEffort(bestEffort);
                }
            }

            unitOfWork.Activities.AddOrEditActivity(activity);
        }

        public void SaveStreams(StreamSet streamSet, long? activityId)
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

            unitOfWork.StreamSets.AddStreamSet(streamSetCopy);

            if (streamSet.Altitude != null)
            {
                streamSet.Altitude.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.AltitudeStreams.AddAltitudeStream(streamSet.Altitude);
            }
            if (streamSet.Distance != null)
            {
                streamSet.Distance.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.DistanceStreams.AddDistanceStream(streamSet.Distance);
            }
            if (streamSet.Latlng != null)
            {
                streamSet.Latlng.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.LatLngStreams.AddLatLngStream(streamSet.Latlng);
            }
            if (streamSet.Time != null)
            {
                streamSet.Time.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.TimeStreams.AddTimeStream(streamSet.Time);
            }
            if (streamSet.VelocitySmooth != null)
            {
                streamSet.VelocitySmooth.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.SmoothVelocityStreams.AddSmoothVelocityStream(streamSet.VelocitySmooth);
            }
            if (streamSet.Heartrate != null)
            {
                streamSet.Heartrate.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.HeartrateStreams.AddHeartrateStream(streamSet.Heartrate);
            }
            if (streamSet.Cadence != null)
            {
                streamSet.Cadence.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.CadenceStreams.AddCadenceStream(streamSet.Cadence);
            }
            if (streamSet.Watts != null)
            {
                streamSet.Watts.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.PowerStreams.AddPowerStream(streamSet.Watts);
            }
            if (streamSet.GradeSmooth != null)
            {
                streamSet.GradeSmooth.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.SmoothGradeStreams.AddSmoothGradeStream(streamSet.GradeSmooth);
            }
            if (streamSet.Moving != null)
            {
                streamSet.Moving.StreamSetId = streamSetCopy.StreamSetId;
                unitOfWork.MovingStreams.AddMovingStream(streamSet.Moving);
            }


        }
    }
}