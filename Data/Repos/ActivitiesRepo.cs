using System.Diagnostics;
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
            // Ensure the MetaAthlete exists or create a new one
            var athlete = context.MetaAthletes.Local.FirstOrDefault(a => a.Id == detailedActivity.Athlete.Id) ??
                          context.MetaAthletes.FirstOrDefault(a => a.Id == detailedActivity.Athlete.Id);
            if (athlete == null)
            {
                athlete = new MetaAthlete
                {
                    Id = detailedActivity.Athlete.Id,
                };
                context.MetaAthletes.Add(athlete);
            }
            detailedActivity.Athlete = athlete;
            detailedActivity.AthleteId = athlete.Id;

            // Ensure the Map is properly attached
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
                        SummaryPolyline = detailedActivity.Map.SummaryPolyline,
                        Activity = detailedActivity,
                        ActivityId = detailedActivity.Id
                    };
                    context.PolylineMaps.Add(map);
                }
                detailedActivity.Map = map;
                detailedActivity.MapId = map.Id;
            }

            // Ensure the SummarySegments are properly attached
            if (detailedActivity.SegmentEfforts != null)
            {
                foreach (var segmentEffort in detailedActivity.SegmentEfforts)
                {
                    var segment = context.Segments.Local.FirstOrDefault(s => s.Id == segmentEffort.Segment.Id) ??
                                  context.Segments.FirstOrDefault(s => s.Id == segmentEffort.Segment.Id);

                    if (segment == null)
                    {
                        segment = new SummarySegment
                        {
                            Id = segmentEffort.Segment.Id,
                            // Set other properties of SummarySegment as needed
                        };
                        context.Segments.Add(segment);
                    }
                    segmentEffort.Segment = segment;
                    segmentEffort.SegmentId = segment.Id;
                    context.Entry(segment).State = EntityState.Detached;

                    context.SegmentEfforts.Add(segmentEffort);
                }
            }

            // Ensure the BestEfforts are properly attached
            if (detailedActivity.BestEfforts != null)
            {
                foreach (var effort in detailedActivity.BestEfforts)
                {
                    effort.DetailedActivity = detailedActivity;
                    effort.ActivityId = detailedActivity.Id;
                    context.SegmentEfforts.Add(effort);
                }
            }

            // Add or update the DetailedActivity object
            context.Activities.Add(detailedActivity);

            //var changes = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();
            //foreach (var entry in changes)
            //{
            //    Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            //    foreach (var prop in entry.OriginalValues.Properties)
            //    {
            //        Debug.WriteLine($"Property: {prop.Name}, Original: {entry.OriginalValues[prop]}, Current: {entry.CurrentValues[prop]}");
            //    }
            //}

            context.SaveChanges();
            DetachActivity(detailedActivity);
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