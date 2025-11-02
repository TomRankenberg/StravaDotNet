using Contracts.Interfaces;
using Data.Models;
using Newtonsoft.Json;

namespace StravaDotNet.Components.Services
{
    public class AuthService(HttpClient client, IStravaUserRepo stravaUserRepo, IConfiguration configuration) : IAuthService
    {
        public async Task<IStravaUser> ValidateCredentialsAsync()
        {
            IStravaUser user = await stravaUserRepo.GetUserByIdAsync(1);
            return user;
        }

        public async Task RemoveCredentialsAsync()
        {
            await stravaUserRepo.RemoveCredentialsByIdAsync(1);
        }

        public async Task<IAccessToken?> GetAccessTokenAsync(string authorizationCode)
        {
            var tokenRequest = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", configuration["StravaUser:ClientId"]),
                new KeyValuePair<string, string>("client_secret", configuration["StravaUser:Secret"]),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            ]);

            var response = await client.PostAsync("https://www.strava.com/oauth/token", tokenRequest);
            var content = await response.Content.ReadAsStringAsync();
            AccessToken? token = JsonConvert.DeserializeObject<AccessToken>(content);

            return token;
        }
    }
}
