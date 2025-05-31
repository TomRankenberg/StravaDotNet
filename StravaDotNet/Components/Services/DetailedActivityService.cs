using Data.Models.Strava;
using Statistics.Models;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DetailedActivityService(HttpClient httpClient)
    {
        public async Task<List<ActivityVm>?> GetDetailedActivityVmsAsync()
        {
            List<ActivityVm>? activityVms = await httpClient.GetFromJsonAsync<List<ActivityVm>>("api/detailedactivities/GetActivityVms");
            if (activityVms == null)
            {
                return null;
            }

            foreach (ActivityVm activityVm in activityVms)
            {
                if (activityVm.Activity != null && activityVm.Activity.Id != null)
                {
                    activityVm.AverageHeartRate = await CalculateAverageHeartRateAsync(activityVm.Activity.Id.Value);
                }
            }

            return activityVms;
        }

        public async Task<List<DetailedActivity>?> GetDetailedActivitiesAsync()
        {
            List<DetailedActivity>? activities = await httpClient.GetFromJsonAsync<List<DetailedActivity>>("api/detailedactivities/GetAllActivities");

            return activities;
        }

        public async Task<double> CalculateAverageHeartRateAsync(long? activityId)
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

        public List<ActivityVm> ConvertStatsToVm(List<ActivityForStats> activityForStats)
        {
            List<ActivityVm> vms = [];
            foreach (ActivityForStats activity in activityForStats)
            {
                ActivityVm vm = new()
                {
                    Activity = new DetailedActivity
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
