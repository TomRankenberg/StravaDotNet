using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Data.Models.Strava;

public class DetailedActivityService
{
    private readonly HttpClient _httpClient;

    public DetailedActivityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DetailedActivity>> GetDetailedActivitiesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DetailedActivity>>("api/detailedactivities");
    }
}
