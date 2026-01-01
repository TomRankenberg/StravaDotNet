using Data.Models.Strava;

namespace StravaDotNet.Components.Services
{
    public class SegmentEffortService(HttpClient httpClient)
    {
        public async Task<List<DetailedSegmentEffort>> GetDetailedSegmentEffortsAsync()
        {
            List<DetailedSegmentEffort>? efforts = await httpClient.GetFromJsonAsync<List<DetailedSegmentEffort>>("api/segmenteffort/GetPRs");
            if (efforts == null)
            {
                return [];
            }
            else
            {
                return efforts;
            }
        }
    }
}