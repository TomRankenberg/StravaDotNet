using Data.Models.Strava;

namespace StravaDotNet.Components.Services
{
    public class SegmentEffortService(HttpClient httpClient)
    {
        public Task<List<DetailedSegmentEffort>?> GetDetailedSegmentEffortsAsync()
        {
            return httpClient.GetFromJsonAsync<List<DetailedSegmentEffort>>("api/segmenteffort/GetPRs");
        }
    }
}