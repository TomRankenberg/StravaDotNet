using Contracts.DTOs;
using Data.Models.Strava;
using Statistics.Models;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DetailedActivityService(HttpClient httpClient)
    {
        private static readonly SemaphoreSlim _semaphore = new(4);

        public async Task<List<ActivityVm>?> GetDetailedActivityVmsAsync(DateTime? from = null, DateTime? to = null)
        {
            string url = "api/detailedactivities/GetActivityVms";
            var queryParams = new List<string>();
            if (from.HasValue)
            {
                queryParams.Add($"from={from.Value:yyyy-MM-dd}");
            }
            if (to.HasValue)
            {
                queryParams.Add($"to={to.Value:yyyy-MM-dd}");
            }
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            List<ActivityVm>? activityVms = await httpClient.GetFromJsonAsync<List<ActivityVm>>(url);
            if (activityVms == null)
            {
                return null;
            }

            var tasks = activityVms
                .Where(vm => vm.Activity != null && vm.Activity.Id != null)
                .Select(async vm =>
                {
                    vm.AverageHeartRate = await CalculateAverageHeartRateAsync(vm.Activity.Id.Value);
                })
                .ToList();

            await Task.WhenAll(tasks);

            return activityVms;
        }

        public async Task<List<ActivityDTO>?> GetDetailedActivitiesAsync()
        {
            List<ActivityDTO>? activities = await httpClient.GetFromJsonAsync<List<ActivityDTO>>("api/detailedactivities/GetAllActivities");

            return activities;
        }

        public async Task<double> CalculateAverageHeartRateAsync(long? activityId)
        {
            await _semaphore.WaitAsync();
            try
            {
                var response = await httpClient.GetAsync($"api/stream/GetHeartStreamFromActivityId?id={activityId}");
                if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    var heartrateStream = await response.Content.ReadFromJsonAsync<HeartrateStream>();
                    if (heartrateStream?.Data != null)
                    {
                        return heartrateStream.Data.Where(x => x != null).Average(x => x ?? 0);
                    }
                }
                return 0;
            }
            finally
            {
                _semaphore.Release();
            }

        }

        public List<ActivityVm> ConvertStatsToVm(List<ActivityForStats> activityForStats)
        {
            List<ActivityVm> vms = [];
            foreach (ActivityForStats activity in activityForStats)
            {
                ActivityVm vm = new()
                {
                    Activity = new ActivityDTO
                    {
                        Id = activity.Id,
                        Type = activity.Type,
                        Distance = activity.Distance,
                        ElapsedTime = activity.ElapsedTime,
                        TotalElevationGain = activity.ElevationGain,
                        StartDate = activity.StartDate,
                        AverageSpeed = activity.AverageSpeed
                    },
                    AverageHeartRate = activity.AverageHeartRate ?? 0,
                    PredictedHR = activity.PredictedHR ?? 0
                };
                vms.Add(vm);
            }
            return vms;
        }
    }
}