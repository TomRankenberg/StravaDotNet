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
        public async Task<List<DetailedSegmentEffort>> GetDetailedSegmentEffortsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<DetailedSegmentEffort>>("api/segmenteffort/GetPRs");
        }

        public async Task<List<HeartrateStream>> GetStreamSets(List<long?> activityIds)
        {
            List<HeartrateStream> heartrateStreams = new List<HeartrateStream>();

            foreach (long? activityId in activityIds)
            {
                var response = await httpClient.GetAsync($"api/stream/GetHeartStreamFromActivityId?id={activityId}");
                if (response.IsSuccessStatusCode && response.StatusCode!= System.Net.HttpStatusCode.NoContent)
                {
                    HeartrateStream? heartrateStream = await response.Content.ReadFromJsonAsync<HeartrateStream>();
                    if (heartrateStream != null)
                    {
                        heartrateStreams.Add(heartrateStream);
                    }
                }
            }

            return heartrateStreams;
        }

    }
}
