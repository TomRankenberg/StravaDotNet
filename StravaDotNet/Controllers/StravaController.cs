using System.Data.SQLite;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Strava.Models;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController : ControllerBase
    {
        private readonly SQLiteConnection _connection;
        private string Token { get; set; }
        private HttpClient HttpClient { get; set; }
        public StravaController(SQLiteConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        [Route("GetActivitiesAsync")]
        public async Task<IActionResult> GetActivitiesAsync(bool includeAllEfforts)
        {
            string path = "https://www.strava.com/api/v3/athlete/activities/";

            string effort = $"?include_all_efforts={includeAllEfforts.ToString().ToLower()}";
            string accessToken = $"&access_token={Token}";

            string url = path + effort + accessToken;

            var response = await new HttpClient().GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                List<DetailedActivity> activities = JsonSerializer.Deserialize<List<DetailedActivity>>(data);
                return Ok(activities);
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    GetAccessToken();
            //    string newUrl = path + effort + "&access_token=" + Token;

            //    var newResponse = await new HttpClient().GetAsync(newUrl);

            //    if (newResponse.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        string data = newResponse.Content.ReadAsStringAsync().Result;

            //        List<DetailedActivity> activities = JsonSerializer.Deserialize<List<DetailedActivity>>(data);
            //        return Ok(activities);
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            else
            {
                return BadRequest();
            }
        }

        //[HttpPost]
        //[Route("GetAccessToken")]
        //public async void GetAccessToken()
        //{
        //    string path = "https://www.strava.com/oauth/token";
        //    TokenRequest tokenRequest = new TokenRequest
        //    {
        //        client_id = "144414",
        //        client_secret = "31fa85c14dc72fee6ebf5bbb9a44f32e625898ad",
        //        code = "62ded79dd894e0296f5f0dfa2e93784d44f69563",
        //        grant_type = "authorization_code"
        //    };

        //    var json = JsonSerializer.Serialize(tokenRequest);
        //    var data = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await new HttpClient().PostAsync(path, data);
        //    var result = response.Content.ReadAsStringAsync().Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        AccessToken tokenResponse = JsonSerializer.Deserialize<AccessToken>(result);
        //        Token = tokenResponse.access_token;
        //    }
        //    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //    {
        //        // get new auth code

        //        // ?client_id=144414&response_type=code&redirect_uri=http://localhost/exchange_token&approval_prompt=force&scope=activity:read_all
        //        string baseUrl = "https://www.strava.com/oauth/authorize";
        //        string redirectUri = "http://localhost:7237/Auth/exchange_token";
        //        string authUrl = $"{baseUrl}?client_id={tokenRequest.client_id}&response_type=code&redirect_uri={redirectUri}&approval_prompt=auto&scope=activity:read_all";

        //        var authCodeResponse = await new HttpClient().GetAsync(authUrl);

        //        if (authCodeResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var authResult = authCodeResponse.Content.ReadAsStringAsync().Result;
        //        }
        //    }
        //}

        //public class TokenRequest
        //{
        //    public string client_id { get; set; }
        //    public string client_secret { get; set; }
        //    public string code { get; set; }
        //    public string grant_type { get; set; }
        //}

        [HttpGet]
        [Route("ConnectToStrava")]
        public IActionResult ConnectToStrava()
        {
            string clientId = "144414";
            string redirectUri = "https://localhost:7237/api/Strava/StravaCallback"; // Your specific callback endpoint
            string state = Guid.NewGuid().ToString();

            var stravaAuthUrl = $"https://www.strava.com/oauth/authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope=read&state={state}";
            return new JsonResult(new { url = stravaAuthUrl });
        }

        // Callback endpoint to handle the response from Strava
        [HttpGet]
        [Route("StravaCallback")]
        public async Task<IActionResult> StravaCallback(string state, string code, string scope)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Missing authorization code.");
            }

            // Exchange the authorization code for an access token
            var token = await GetAccessTokenAsync(code);
            if (token == null)
            {
                return BadRequest("Error getting access token.");
            }
            else
            {
                Token = token.access_token;
            }

            // Save the token and use it to access Strava API
            // (Store token appropriately based on your application's needs)
            return Ok();
        }

        public async Task<AccessToken> GetAccessTokenAsync(string authorizationCode)
        {
            var client = new HttpClient();
            var tokenRequest = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", "144414"),
                new KeyValuePair<string, string>("client_secret", "31fa85c14dc72fee6ebf5bbb9a44f32e625898ad"),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

            var response = await client.PostAsync("https://www.strava.com/oauth/token", tokenRequest);
            var content = await response.Content.ReadAsStringAsync();
            AccessToken token = JsonSerializer.Deserialize<AccessToken>(content);

            return token;
        }
    }
}