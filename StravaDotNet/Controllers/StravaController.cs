using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Strava.Models;
using static System.Net.WebRequestMethods;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController : ControllerBase
    {
        //public StravaController(string accessToken, HttpClient httpClient)
        //{
        //    AccessToken = accessToken;
        //    HttpClient = httpClient;
        //}

        private string Token { get; set; }
        private HttpClient HttpClient { get; set; }
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
            var response = await HttpClient.SendAsync(request);

            // Check the response status
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DetailedActivity>(content);
        }
        [HttpGet]
        [Route("GetActivitiesAsync")]
        public async Task<IActionResult> GetActivitiesAsync(bool includeAllEfforts)
        {
            string path = "https://www.strava.com/api/v3/athlete/activities/";

            string effort = $"?include_all_efforts={includeAllEfforts.ToString().ToLower()}";
            string accessToken = "&access_token=c1715239176fc9201dafb8bcaefcc0f807ea75d4";              

            string url = path + effort + accessToken;

            var response = await new HttpClient().GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                List<DetailedActivity> activities = JsonSerializer.Deserialize<List<DetailedActivity>>(data);
                return Ok(activities);
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                GetAccessToken();
                string newUrl = path + effort + "&access_token=" + Token;

                var newResponse = await new HttpClient().GetAsync(newUrl);

                if (newResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string data = newResponse.Content.ReadAsStringAsync().Result;

                    List<DetailedActivity> activities = JsonSerializer.Deserialize<List<DetailedActivity>>(data);
                    return Ok(activities);
                }
                else
                {
                    return BadRequest();
                }
            }

            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("GetAccessToken")]
        public async void GetAccessToken()
        {
            string path = "https://www.strava.com/oauth/token";
            TokenRequest tokenRequest = new TokenRequest
            {
                client_id = "144414",
                client_secret = "31fa85c14dc72fee6ebf5bbb9a44f32e625898ad",
                code = "62ded79dd894e0296f5f0dfa2e93784d44f69563",
                grant_type = "authorization_code"
            };

            var json = JsonSerializer.Serialize(tokenRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PostAsync(path, data);
            var result = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                AccessToken tokenResponse = JsonSerializer.Deserialize<AccessToken>(result);
                Token = tokenResponse.Access_token;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // get new auth code

                // ?client_id=144414&response_type=code&redirect_uri=http://localhost/exchange_token&approval_prompt=force&scope=activity:read_all
                string baseUrl = "https://www.strava.com/oauth/authorize";
                string redirectUri = "http://localhost:7237/Auth/exchange_token";
                string authUrl = $"{baseUrl}?client_id={tokenRequest.client_id}&response_type=code&redirect_uri={redirectUri}&approval_prompt=auto&scope=activity:read_all";

                var authCodeResponse = await new HttpClient().GetAsync(authUrl);

                if (authCodeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var authResult = authCodeResponse.Content.ReadAsStringAsync().Result;
                }

            }
        }
        [HttpGet]
        [Route("GetAuthCode")]
        public async Task<IActionResult> GetAuthCode()
        {
            string path = "https://www.strava.com/oauth/authorize";
            var queryParams = new List<string>();
            queryParams.Add("client_id=144414");
            queryParams.Add("response_type=code");
            queryParams.Add("redirect_uri=http://localhost:5000/api/strava/getaccesstoken");
            queryParams.Add("approval_prompt=auto");
            queryParams.Add("scope=read,activity:read_all");
            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams);
            return Redirect(path);
        }
        public class TokenRequest
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string code { get; set; }
            public string grant_type { get; set; }
        }

        [Route("[controller]")]
        public class AuthController : Controller
        {
            [HttpGet("exchange_token")]
            public IActionResult ExchangeToken(string code)
            { // Do something with the authorization code 
                // For now, just returning it as part of the response
                return Ok(new { Code = code }); 
            } 
        }
    }
}