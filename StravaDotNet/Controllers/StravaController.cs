using Newtonsoft.Json;
using Data.Interfaces;
using Data.Models;
using Data.Models.Strava;
using Microsoft.AspNetCore.Mvc;
using DataManagement.BusinessLogic;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController(IStravaUserRepo userRepo, IActivitiesRepo activityRepo, IAthleteRepo athleteRepo, IMapRepo mapRepo,
        ISegmentRepo segmentRepo, ISegmentEffortRepo segmentEffortRepo) : ControllerBase
    {
        private string Token { get; set; }
        private HttpClient HttpClient { get; set; }

        [HttpGet]
        [Route("GetActivitiesAsync")]
        public async Task<IActionResult> GetActivitiesAsync(int? after)
        {
            if (Token == null)
            {
                StravaUser stravaUser = userRepo.GetUserById(1);
                Token = stravaUser.AccessToken;
            }
            string accessToken = $"?access_token={Token}";

            string afterString = "";
            if (after != null)
            {
                afterString = $"&after={after}";
            }

            string activitiesToGet = "&per_page=50";
            int page = 1;
            string numberOfPages = $"&page={page}";
            string path = "https://www.strava.com/api/v3/athlete/activities/";

            string url = path + accessToken + activitiesToGet + numberOfPages + afterString;

            var response = await new HttpClient().GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                List<DetailedActivity> activities = JsonConvert.DeserializeObject<List<DetailedActivity>>(data);
                DataSaver dataSaver = new DataSaver(activityRepo, athleteRepo, mapRepo, segmentRepo, segmentEffortRepo);
                foreach(DetailedActivity activity in activities)
                {
                    IActionResult detailedActivityResponse = await GetActivityById(activity.Id);
                    if (detailedActivityResponse is OkObjectResult okResult)
                    {
                        if (okResult.Value is DetailedActivity detailedActivity)
                        {
                            try
                            {
                                dataSaver.SaveActivity(detailedActivity, detailedActivity.Athlete.Id);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }                
                while (activities.Count == 50 && after == null)
                {
                    page++;
                    numberOfPages = $"&page={page}";
                    url = path + accessToken + activitiesToGet + numberOfPages;
                    response = await new HttpClient().GetAsync(url);
                    data = response.Content.ReadAsStringAsync().Result;
                    activities = JsonConvert.DeserializeObject<List<DetailedActivity>>(data);
                    foreach (DetailedActivity activity in activities)
                    {
                        IActionResult detailedActivityResponse = await GetActivityById(activity.Id);
                        if (detailedActivityResponse is OkObjectResult okResult)
                        {
                            if (okResult.Value is DetailedActivity detailedActivity)
                            {
                                try
                                {
                                    dataSaver.SaveActivity(detailedActivity, detailedActivity.Athlete.Id);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }

                return Ok(activities);
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;

                return BadRequest();
            }
        }

        public async Task<IActionResult> GetActivityById(long? activityId)
        {
            if (Token == null)
            {
                StravaUser stravaUser = userRepo.GetUserById(1);
                Token = stravaUser.AccessToken;
            }
            string accessToken = $"&access_token={Token}";

            string path = $"https://www.strava.com/api/v3/activities/{activityId}?include_all_efforts=true";
            string url = path + accessToken;
            var response = await new HttpClient().GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                DetailedActivity activity = JsonConvert.DeserializeObject<DetailedActivity>(data);
                return Ok(activity);
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                while(data.Contains("Rate Limit Exceeded"))
                {
                    Thread.Sleep(960000);// wait 16 minutes
                    response = await new HttpClient().GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        DetailedActivity activity = JsonConvert.DeserializeObject<DetailedActivity>(data);
                        return Ok(activity);
                    }
                }
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetStreamsForActivity(long? activityId)
        {
            if (Token == null)
            {
                StravaUser stravaUser = userRepo.GetUserById(1);
                Token = stravaUser.AccessToken;
            }
            string accessToken = $"&access_token={Token}";

            //string path = $"https://www.strava.com/api/v3/activities/{activityId}?include_all_efforts=true";
            string path = $"https://www.strava.com/api/v3/activities/{activityId}/streams?keys=&key_by_type=";
            string url = path + accessToken;
            var response = await new HttpClient().GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                StreamSet activity = JsonConvert.DeserializeObject<StreamSet>(data);
                return Ok(activity);
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                while (data.Contains("Rate Limit Exceeded"))
                {
                    Thread.Sleep(960000);// wait 16 minutes
                    response = await new HttpClient().GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        StreamSet activity = JsonConvert.DeserializeObject<StreamSet>(data);
                        return Ok(activity);
                    }
                }
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("ConnectToStrava")]
        public IActionResult ConnectToStrava()
        {
            string clientId = "144414";
            string redirectUri = "https://localhost:7237/api/Strava/StravaCallback"; // Your specific callback endpoint
            string state = Guid.NewGuid().ToString();

            var stravaAuthUrl = $"https://www.strava.com/oauth/authorize?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope=read,activity:read_all&state={state}";
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
                StravaUser stravaUser = userRepo.GetUserById(1);

                if (stravaUser == null)
                {
                    stravaUser = new StravaUser
                    {
                        UserId = 1,
                        AccessToken = token.access_token,
                        RefreshToken = token.refresh_token,
                        AccessTokenExpiresAt = token.expires_at.ToString(),
                        Secret = "31fa85c14dc72fee6ebf5bbb9a44f32e625898ad",
                        StravaId = 144414,
                        Email = "tomrankenberg@live.nl"
                    };
                    userRepo.AddUser(stravaUser);
                }
                else
                {
                    stravaUser.AccessToken = token.access_token;
                    stravaUser.RefreshToken = token.refresh_token;
                    stravaUser.AccessTokenExpiresAt = token.expires_at.ToString();
                    userRepo.UpdateUser(stravaUser);
                }
            }

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
            AccessToken token = JsonConvert.DeserializeObject<AccessToken>(content);

            return token;
        }
    }
}