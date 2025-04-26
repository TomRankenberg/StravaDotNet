using Data.Models.Strava;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class DetailedActivityService(HttpClient httpClient)
    {
        public async Task<List<ActivityVm>> GetDetailedActivitiesAsync()
        {
            List<ActivityVm> activityVms = await httpClient.GetFromJsonAsync<List<ActivityVm>>("api/detailedactivities");

            foreach (ActivityVm activityVm in activityVms)
            {
                if (activityVm.Activity != null && activityVm.Activity.Id != null)
                {
                    var response = await httpClient.GetAsync($"api/stream/GetHeartStreamFromActivityId?id={activityVm.Activity.Id}");
                    if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        HeartrateStream? heartrateStream = await response.Content.ReadFromJsonAsync<HeartrateStream>();
                        if (heartrateStream?.Data != null)
                        {
                            double avgHeartRate = heartrateStream.Data.Where(x => x != null).Average(x => x ?? 0);
                            activityVm.AverageHeartRate = Math.Round(avgHeartRate, 2);
                        }
                    }
                }
            }

            return activityVms;
        }

        public async Task<double> CalculateAverageHeartRateAsync(long activityId)
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
    }

}
