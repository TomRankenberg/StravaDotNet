using Contracts.DTOs;
using Data.Models.Strava;
using Newtonsoft.Json;
using Statistics.BusinessLogic;
using Statistics.Models;
using System.Text;

namespace StravaDotNet.Components.Services
{
    public class StatsService(HttpClient httpClient, DetailedActivityService detailedActivityService)
    {
        public async Task<string> PredictAsync(List<ActivityDTO> activities)
        {// Doesnt work yet, not implemented
            List<ActivityForStats> activityStatsList = [];

            foreach (ActivityDTO activity in activities.Where(a => a.Id.HasValue))
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

        public async Task<List<ActivityForStats>> PredictStaticAsync(List<ActivityDTO> activities)
        {
            List<ActivityForStats> activityStatsList = [];

            foreach (ActivityDTO activity in activities.Where(a => a.Id.HasValue))
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
                activityStatsList.PredictedHR.HasValue && activityStatsList.PredictedHR.Value > 0 &&
                activityStatsList.AverageHeartRate.HasValue && activityStatsList.AverageHeartRate.Value > 0)
                .ToList();

            return activityStatsList;
        }

        public StatsVm CalculateSummaryStats(List<ActivityForStats> activities)
        {
            double[] measured = activities
                .Where(a => a.AverageHeartRate.HasValue && a.PredictedHR.HasValue)
                .Select(a => a.AverageHeartRate.Value)
                .ToArray();
            double[] predicted = activities
                .Where(a => a.AverageHeartRate.HasValue && a.PredictedHR.HasValue)
                .Select(a => a.PredictedHR.Value)
                .ToArray();

            if (measured.Length == 0 || predicted.Length == 0)
            {
                return new StatsVm();
            }

            double mse = measured.Zip(predicted, (m, p) => Math.Pow(m - p, 2)).Average();
            double rmse = Math.Sqrt(mse);
            double avgError = measured.Zip(predicted, (m, p) => Math.Abs(m - p)).Average();

            double meanMeasured = measured.Average();
            double ssTot = measured.Select(m => Math.Pow(m - meanMeasured, 2)).Sum();
            double ssRes = measured.Zip(predicted, (m, p) => Math.Pow(m - p, 2)).Sum();
            double r2 = 1 - (ssRes / ssTot);

            return new StatsVm
            {
                RMSE = rmse,
                R2 = r2,
                AverageError = avgError
            };
        }
    }
}