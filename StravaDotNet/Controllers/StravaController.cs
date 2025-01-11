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
        private string AccessToken { get; set; }
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
        [HttpGet]
        public async Task<IActionResult> GetActivitiesAsync(bool? includeAllEfforts)
        {
            string path = "https://www.strava.com/api/v3/athlete/activities/";
            var queryParams = new List<string>();
            queryParams.Add("access_token=45417675ed95fa0b3d117e29b2dbea6dac07f33c");

            if (includeAllEfforts.HasValue) queryParams.Add($"include_all_efforts={includeAllEfforts.Value.ToString().ToLower()}");

            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams);

            var response = await httpClient.GetAsync(path);
            var data = response.Content.ReadAsStringAsync().Result;
            List<DetailedActivity> activities = JsonConvert.DeserializeObject<List<DetailedActivity>>(data);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            return Ok(activities);

        }
        [HttpPost]
        public async Task<IActionResult> GetAccessToken()
        {
            string path = "https://www.strava.com/oauth/token";
            TokenRequest tokenRequest = new TokenRequest
            {
                client_id = "144414",
                client_secret = "31fa85c14dc72fee6ebf5bbb9a44f32e625898ad",
                code = "62ded79dd894e0296f5f0dfa2e93784d44f69563",
                grant_type = "authorization_code"
            };

            var json = JsonConvert.SerializeObject(tokenRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(path, data);
            var result = response.Content.ReadAsStringAsync().Result;


            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            else
            {
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                AccessToken = tokenResponse.access_token;

                return Ok(result);
            }
        }
        public class TokenRequest
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string code { get; set; }
            public string grant_type { get; set; }
        }
        public class TokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_at { get; set; }
            public string expires_in { get; set; }
            public string refresh_token { get; set; }
            public SummaryAthlete athlete { get; set; }
        }
    }
}