using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Strava.Models;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController(HttpClient httpClient) : Controller
    {
        public async Task<DetailedActivity> GetActivityByIdAsync(long? id, bool? includeAllEfforts)
        {
            // Verify the required parameter 'id' is set
            //if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetActivityById");

            //var path = $"/activities/{id}";
            var path = "/activities/144414";
            var queryParams = new List<string>();

            if (includeAllEfforts.HasValue) queryParams.Add($"include_all_efforts={includeAllEfforts.Value.ToString().ToLower()}");

            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams); 
            
            // Set up the HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, path); // Set the authorization header (assumes Bearer token is used)

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "99e020164feb3c7a3aa0d9407a35f475867fa970"); 
            
            // Send the request
            var response = await httpClient.SendAsync(request);
            
            // Check the response status
            //if (!response.IsSuccessStatusCode) throw new ApiException((int)response.StatusCode, $"Error calling GetActivityById: {response.ReasonPhrase}");
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<DetailedActivity>(content);
        }        
    }
}