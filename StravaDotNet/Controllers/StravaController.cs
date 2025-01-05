using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Strava.Models;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController(HttpClient httpClient) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<DetailedActivity> GetAthlete(long id, bool? includeAllEfforts)
        {
            //var path = "https://www.strava.com/api/v3/activities/144414/";
            var path = "https://www.strava.com/api/v3/athlete";
            var queryParams = new List<string>();

            if (includeAllEfforts.HasValue) queryParams.Add($"include_all_efforts={includeAllEfforts.Value.ToString().ToLower()}");

            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams);

            // Set up the HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Post, path); // Set the authorization header (assumes Bearer token is used)

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "99e020164feb3c7a3aa0d9407a35f475867fa970");

            // Send the request
            var response = await httpClient.SendAsync(request);

            // Check the response status
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DetailedActivity>(content);
        }
        [HttpPost]
        public async Task<IActionResult> GetaActivitiesAsync(bool? includeAllEfforts)
        {
            ActivityPayload activityPayload = new ActivityPayload
            {
                Id = 1,
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(activityPayload), Encoding.UTF8, "application/json");

            string path = "https://www.strava.com/api/v3/activities/144414/";
            var queryParams = new List<string>();

            if (includeAllEfforts.HasValue) queryParams.Add($"include_all_efforts={includeAllEfforts.Value.ToString().ToLower()}");

            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams);

            var response = await httpClient.PostAsync("https://www.strava.com/api/v3/activities", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            return Ok(response);

        }
        public class ActivityPayload
        {
            public long Id { get; set; }
            public string Secret { get; set; }
            public string RefreshToken { get; set; }
            public string TokenType { get; set; }
        }
    }
}