using Data.Context;
using Data.Interfaces;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ActivitiesRepo(DatabaseContext context) : IActivitiesRepo
    {
        public void AddActivity(DetailedActivity detailedActivity)
        {
            // Retrieve or create the MetaAthlete object
            var athlete = context.MetaAthletes.Local.FirstOrDefault(a => a.Id == detailedActivity.Athlete.Id) ??
                          context.MetaAthletes.FirstOrDefault(a => a.Id == detailedActivity.Athlete.Id);
            if (athlete == null)
            {
                athlete = new MetaAthlete
                {
                    Id = detailedActivity.Athlete.Id,
                };
                context.MetaAthletes.Add(athlete);
                context.SaveChanges();
            }
            else
            {
                detailedActivity.Athlete = athlete;
            }

            // Set the AthleteId property
            detailedActivity.AthleteId = athlete.Id;

            // Set other foreign key properties if needed
            // For example, if you have a foreign key for PolylineMap
            if (detailedActivity.Map != null)
            {
                var map = context.PolylineMaps.Local.FirstOrDefault(m => m.Id == detailedActivity.Map.Id) ??
                          context.PolylineMaps.FirstOrDefault(m => m.Id == detailedActivity.Map.Id);

                if (map == null)
                {
                    map = new PolylineMap
                    {
                        Id = detailedActivity.Map.Id,
                        Polyline = detailedActivity.Map.Polyline,
                        SummaryPolyline = detailedActivity.Map.Polyline,
                        Activity = detailedActivity,
                        ActivityId = detailedActivity.Id
                    };
                    context.PolylineMaps.Add(map);
                    context.SaveChanges();
                }
                else
                {
                    detailedActivity.MapId = map.Id;
                    map.Polyline = detailedActivity.Map.Polyline;
                    map.SummaryPolyline = detailedActivity.Map.Polyline;
                    map.Activity = detailedActivity;
                    map.ActivityId = detailedActivity.Id;
                    detailedActivity.Map = map;
                }
            }

            // Add or update the DetailedActivity object
            var existingActivity = context.Activities
                .Include(a => a.SegmentEfforts)
                .Include(a => a.Laps)
                .FirstOrDefault(a => a.Id == detailedActivity.Id);

            if (existingActivity != null)
            {
                context.Entry(existingActivity).CurrentValues.SetValues(detailedActivity);

                // Update SegmentEfforts
                foreach (var segmentEffort in detailedActivity.SegmentEfforts)
                {
                    var existingSegmentEffort = existingActivity.SegmentEfforts
                        .FirstOrDefault(se => se.Id == segmentEffort.Id);
                    if (existingSegmentEffort != null)
                    {
                        context.Entry(existingSegmentEffort).CurrentValues.SetValues(segmentEffort);
                    }
                    else
                    {
                        existingActivity.SegmentEfforts.Add(segmentEffort);
                    }
                }

                // Update Laps
                foreach (var lap in detailedActivity.Laps)
                {
                    var existingLap = existingActivity.Laps
                        .FirstOrDefault(l => l.Id == lap.Id);
                    if (existingLap != null)
                    {
                        context.Entry(existingLap).CurrentValues.SetValues(lap);
                    }
                    else
                    {
                        existingActivity.Laps.Add(lap);
                    }
                }
            }
            else
            {
                context.Activities.Add(detailedActivity);
            }

            context.SaveChanges();
        }



        public DetailedActivity GetActivityById(int id)
        {
            return context.Activities.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateActivity(DetailedActivity detailedActivity)
        {
            context.Activities.Update(detailedActivity);
            context.SaveChanges();
        }

        public List<int> GetAllActivityIds()
        {
            List<int> ids = [];
            foreach (var activity in context.Activities)
            {
                int id = (int)activity.Id;
                ids.Add(id);
            }
            return ids;
        }

        public void DetachActivity(DetailedActivity detailedActivity)
        {
            context.Entry(detailedActivity).State = EntityState.Detached;
        }

        public List<DetailedActivity> GetAllActivities()
        {
            return context.Activities.ToList();
        }
    }
}