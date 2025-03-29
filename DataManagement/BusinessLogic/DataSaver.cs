using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;

namespace DataManagement.BusinessLogic
{
    public class DataSaver(IActivitiesRepo activityRepo, IAthleteRepo athleteRepo, IMapRepo mapRepo, ISegmentRepo segmentRepo, ISegmentEffortRepo segmentEffortRepo)
    {
        public string SaveActivities(List<DetailedActivity> activities)
        {
            MetaAthlete athlete = activities.FirstOrDefault().Athlete;
            athleteRepo.AddOrEditAthlete(athlete);

            int savingCounter = 0;
            foreach (DetailedActivity activity in activities)
            {
                activity.AthleteId = athlete.Id;
                activity.Athlete = null;

                if (activity.Map != null)
                {
                    activity.MapId = mapRepo.AddOrEditMap(activity.Map);
                    activity.Map = null;
                }

                if (activity.SegmentEfforts != null)
                {
                    foreach (DetailedSegmentEffort segmentEffort in activity.SegmentEfforts)
                    {
                        long? segmentId = segmentRepo.AddOrEditSegment(segmentEffort.Segment);
                        segmentEffort.SegmentId = segmentId;
                        segmentEffort.Segment = null;

                        segmentEffort.ActivityId = activity.Id;
                        segmentEffortRepo.AddOrEditSegmentEffort(segmentEffort);
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
                        segmentEffortRepo.AddOrEditSegmentEffort(bestEffort);
                    }
                }

                activityRepo.AddOrEditActivity(activity);
                savingCounter++;
            }
            return $"Saved {savingCounter} activities";
        }
    }
}