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
    }
}
