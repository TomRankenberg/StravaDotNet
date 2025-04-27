using Data.Models.Strava;

namespace StravaDotNet.Components.Services
{
    public class SegmentEffortService(HttpClient httpClient)
    {
        public List<DetailedSegmentEffort> GetDetailedSegmentEffortsAsync()
        {
            List<DetailedSegmentEffort> segmentEfforts = httpClient.GetFromJsonAsync<List<DetailedSegmentEffort>>("api/segmenteffort/GetPRs").Result;

            return segmentEfforts;
        }
    }
}