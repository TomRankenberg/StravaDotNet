using Contracts.Interfaces;
using Data.Models.Strava;
using Newtonsoft.Json;

namespace StravaDotNet.Components.Services
{
    public class StravaService(HttpClient client, IStravaUserRepo userRepo) : IStravaService
    {
        public async Task<string?> RetrieveActivities(string? token, long after)
        {
            string tokenString = $"?access_token={token}";
            string afterString = $"&after={after}";

            string activitiesToGet = "&per_page=50";
            int page = 1;
            string numberOfPages = $"&page={page}";
            string path = "https://www.strava.com/api/v3/athlete/activities/";

            string url = path + tokenString + activitiesToGet + numberOfPages + afterString;

            HttpResponseMessage response = await client.GetAsync(url);

            List<DetailedActivity>? activities = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                activities = JsonConvert.DeserializeObject<List<DetailedActivity>>(data);

                while (activities != null && activities.Count == 50)
                {
                    page++;
                    numberOfPages = $"&page={page}";
                    url = path + tokenString + activitiesToGet + numberOfPages;
                    response = await new HttpClient().GetAsync(url);
                    data = response.Content.ReadAsStringAsync().Result;
                    List<DetailedActivity>? extraActivities = JsonConvert.DeserializeObject<List<DetailedActivity>>(data);
                    if (extraActivities == null)
                    {
                        break;
                    }
                    else
                    {
                        activities.AddRange(extraActivities);
                    }
                }
            }
            string activitiesJson = JsonConvert.SerializeObject(activities);
            return activitiesJson;
        }

        public async Task<string?> GetActivityById(long? activityId, string? token)
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
                string activity = response.Content.ReadAsStringAsync().Result;
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
                        string activity = response.Content.ReadAsStringAsync().Result;
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