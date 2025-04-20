using Data.Models.Strava;

namespace StravaDotNet.Components.Services
{
    public class DetailedActivityService(HttpClient httpClient)
    {
        public async Task<List<DetailedActivity>> GetDetailedActivitiesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<DetailedActivity>>("api/detailedactivities");
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
