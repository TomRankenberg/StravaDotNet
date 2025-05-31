using Data.Models.Strava;
using Newtonsoft.Json;
using Statistics.BusinessLogic;
using Statistics.Models;
using System.Text;

namespace StravaDotNet.Components.Services
{
    public class StatsService(HttpClient httpClient, DetailedActivityService detailedActivityService)
    {
        public async Task<string> PredictAsync(List<DetailedActivity> activities)
        {// Doesnt work yet, not implemented
            List<ActivityForStats> activityStatsList = [];

            foreach (DetailedActivity activity in activities.Where(a => a.Id.HasValue))
            {
                double avgHeartRate = await detailedActivityService.CalculateAverageHeartRateAsync(activity.Id);
                ActivityForStats? activityStats = ActivityStatsConverter.ConvertToActivityForStats(activity, avgHeartRate);
                if (activityStats != null)
                {
                    activityStatsList.Add(activityStats);
                }
            }
            activityStatsList = ActivityStatsConverter.AddRecentTimeSpent(activityStatsList, 60);
            activityStatsList = ActivityStatsConverter.AddPredictedHeartRate(activityStatsList);
            var json = JsonConvert.SerializeObject(activityStatsList);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:5000/predict", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<ActivityForStats>> PredictStaticAsync(List<DetailedActivity> activities)
        {
            List<ActivityForStats> activityStatsList = [];

            foreach (DetailedActivity activity in activities.Where(a => a.Id.HasValue))
            {
                double avgHeartRate = await detailedActivityService.CalculateAverageHeartRateAsync(activity.Id);
                ActivityForStats? activityStats = ActivityStatsConverter.ConvertToActivityForStats(activity, avgHeartRate);
                if (activityStats != null)
                {
                    activityStatsList.Add(activityStats);
                }
            }
            activityStatsList = ActivityStatsConverter.AddRecentTimeSpent(activityStatsList, 60);
            activityStatsList = ActivityStatsConverter.AddPredictedHeartRate(activityStatsList);
            activityStatsList = activityStatsList
                .Where(activityStatsList => 
                activityStatsList.PredictedHR.HasValue && activityStatsList.PredictedHR.Value > 100 &&
                activityStatsList.AverageHeartRate.HasValue && activityStatsList.AverageHeartRate.Value > 0)
                .ToList();

            return activityStatsList;
        }
    }
}