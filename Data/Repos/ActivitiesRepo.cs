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
            // Athlete
            detailedActivity.AthleteId = detailedActivity.Athlete.Id;
            detailedActivity.Athlete = null;

            // Map
            if (detailedActivity.Map != null)
            {
                detailedActivity.MapId = detailedActivity.Map.Id;
                detailedActivity.Map.ActivityId = detailedActivity.Id;
                detailedActivity.Map.Activity = detailedActivity;
            }
            
            // Segments
            if (detailedActivity.SegmentEfforts != null)
            {
                foreach (DetailedSegmentEffort segmentEffort in detailedActivity.SegmentEfforts)
                {
                    segmentEffort.DetailedActivity = detailedActivity;
                    segmentEffort.ActivityId = detailedActivity.Id;

                }
            }

            // Best efforts
            if (detailedActivity.BestEfforts != null)
            {
                foreach (DetailedSegmentEffort effort in detailedActivity.BestEfforts)
                {
                    effort.ActivityId = detailedActivity.Id;
                    effort.DetailedActivity = detailedActivity;
                }
            }

            // Add or update the DetailedActivity object
            context.Activities.Add(detailedActivity);

            var changes = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();
            foreach (var entry in changes)
            {
                Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                foreach (var prop in entry.OriginalValues.Properties)
                {
                    Debug.WriteLine($"Property: {prop.Name}, Original: {entry.OriginalValues[prop]}, Current: {entry.CurrentValues[prop]}");
                }
            }
            
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