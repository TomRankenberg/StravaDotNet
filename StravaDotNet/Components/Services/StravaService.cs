using Contracts.Interfaces;
using Data.Models.Strava;
using Newtonsoft.Json;

namespace StravaDotNet.Components.Services
{
    public class StravaService(HttpClient client, IStravaUserRepo userRepo) : IStravaService
    {
        public async Task<IDetailedActivity?> GetActivityById(long? activityId, string? token)
        {
            if (token == null)
            {
                IStravaUser stravaUser = await userRepo.GetUserByIdAsync(1);
                token = stravaUser.AccessToken;
            }
            string accessToken = $"&access_token={token}";

            string path = $"https://www.strava.com/api/v3/activities/{activityId}?include_all_efforts=true";
            string url = path + accessToken;
            var response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                DetailedActivity? activity = JsonConvert.DeserializeObject<DetailedActivity>(data);
                return activity;
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                while (data.Contains("Rate Limit Exceeded"))
                {
                    Thread.Sleep(960000);// wait 16 minutes
                    response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        DetailedActivity? activity = JsonConvert.DeserializeObject<DetailedActivity>(data);
                        return activity;
                    }
                }
                return null;
            }
        }

        public async Task<IStreamSet?> GetStreamsForActivity(long? activityId, string? token)
        {
            if (token == null)
            {
                IStravaUser stravaUser = await userRepo.GetUserByIdAsync(1);
                token = stravaUser.AccessToken;
            }
            string accessToken = $"&access_token={token}";

            string path = $"https://www.strava.com/api/v3/activities/{activityId}/streams?keys=time,heartrate,distance,altitude,temp&key_by_type=true";
            string url = path + accessToken;
            var response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                StreamSet? activity = JsonConvert.DeserializeObject<StreamSet>(data);
                return activity;
            }
            else
            {
                string data = response.Content.ReadAsStringAsync().Result;
                while (data.Contains("Rate Limit Exceeded"))
                {
                    Thread.Sleep(960000);// wait 16 minutes
                    response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                        StreamSet? activity = JsonConvert.DeserializeObject<StreamSet>(data);
                        return activity;
                    }
                }
                return null;
            }
        }
    }
}