using Data.Models.Strava;
using DataManagement.Models;
using Newtonsoft.Json;
using Statistics.BusinessLogic;
using System.Text;

namespace StravaDotNet.Components.Services
{
    public class PythonService
    {
        private readonly HttpClient _httpClient;
        private readonly DetailedActivityService _detailedActivityService;

        public PythonService(HttpClient httpClient, DetailedActivityService detailedActivityService)
        {
            _httpClient = httpClient;
            _detailedActivityService = detailedActivityService;
        }

        public async Task<string> PredictAsync(List<DetailedActivity> activities)
        {// Doesnt work yet, not implemented
            List<ActivityForStats> activityStatsList = [];

            foreach (DetailedActivity activity in activities.Where(a => a.Id.HasValue))
            {
                double avgHeartRate = await _detailedActivityService.CalculateAverageHeartRateAsync(activity.Id.Value);
                ActivityForStats activityStats = ActivityStatsConverter.ConvertToActivityForStats(activity, avgHeartRate);
                activityStatsList.Add(activityStats);
            }
            // Calculate recent activity time here
            var json = JsonConvert.SerializeObject(activityStatsList);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/predict", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
