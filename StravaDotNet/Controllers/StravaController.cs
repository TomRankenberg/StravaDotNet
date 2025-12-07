using Contracts.Interfaces;
using Data.BusinessLogic;
using Data.Models;
using Data.Models.Strava;
using Microsoft.AspNetCore.Mvc;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController(IStravaUserRepo userRepo, IUnitOfWork unitOfWork, IConfiguration configuration,
        IAuthService authService, IStravaService stravaService, ISavingService savingService) : ControllerBase
    {
        private string? Token { get; set; }

        [HttpGet]
        [Route("GetActivitiesAsync")]
        public async Task<IActionResult> GetActivitiesAsync()
        {
            if (Token == null)
            {
                IStravaUser stravaUser = await userRepo.GetUserByIdAsync(1);
                Token = stravaUser.AccessToken;
            }
            DateTime? latestActivityTime = await unitOfWork.Activities.GetLatestActivityTimeAsync();
            long after = latestActivityTime != null ? ((DateTimeOffset)latestActivityTime.Value).ToUnixTimeSeconds() : 0;
            string? activitiesJson = await stravaService.RetrieveActivities(Token, after);

            if (activitiesJson != null )
            {
                await savingService.SaveActivities(activitiesJson, Token);
                return Ok(activitiesJson);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetStreamsAsync")]
        public async Task<IActionResult> GetStreamsAsync()
        {
            DataSaver dataSaver = new(unitOfWork);
            List<long?> activityIds = unitOfWork.Activities.GetAllActivityIds();
            List<long?> activityIdsFromStreams = await unitOfWork.StreamSets.GetAllActivityIdsFromStreamSetsAsync();
            foreach (long? activityId in activityIds)
            {
                if (!activityIdsFromStreams.Contains(activityId))
                {
                    IStreamSet? streamResult = await stravaService.GetStreamsForActivity(activityId, Token);
                    if (streamResult != null)
                    {
                        try
                        {
                            await dataSaver.SaveStreams((StreamSet)streamResult, activityId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        return BadRequest($"Failed to retrieve streams for activity ID: {activityId}");
                    }
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("ConnectToStrava")]
        public IActionResult ConnectToStrava()
        {
            string clientId = configuration["StravaUser:ClientId"];
            string baseUrl = configuration["AppSettings:BaseAddress"];
            string redirectUri = $"{baseUrl}/api/Strava/StravaCallback";
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
            var token = await authService.GetAccessTokenAsync(code);
            if (token == null)
            {
                return BadRequest("Error getting access token.");
            }
            else
            {
                Token = token.access_token;
                IStravaUser stravaUser = await userRepo.GetUserByIdAsync(1);

                if (stravaUser == null)
                {
                    stravaUser = new StravaUser
                    {
                        UserId = 1,
                        AccessToken = token.access_token,
                        RefreshToken = token.refresh_token,
                        AccessTokenExpiresAt = token.expires_at.ToString()
                    };
                    await userRepo.AddUserAsync(stravaUser);
                }
                else
                {
                    stravaUser.AccessToken = token.access_token;
                    stravaUser.RefreshToken = token.refresh_token;
                    stravaUser.AccessTokenExpiresAt = token.expires_at.ToString();
                    await userRepo.UpdateUserAsync(stravaUser);
                }
            }

            // Return HTML that closes the window
            string html = @"
                <html>
                    <head>
                        <title>Strava Authentication</title>
                    </head>
                    <body>
                        <script>
                            window.close();
                        </script>
                        <p>You can close this window.</p>
                    </body>
                </html>";
            return Content(html, "text/html");
        }
    }
}