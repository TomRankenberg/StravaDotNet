using Microsoft.AspNetCore.Mvc;
using Strava.Models;

namespace StravaDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StravaController : Controller
    {
        private readonly HttpClient _httpClient;
        public StravaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DetailedActivity> GetActivityByIdAsync(long? id, bool? includeAllEfforts)
        { // Verify the required parameter 'id' is set
          if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetActivityById"); 
            var path = $"/activities/{id}"; 
            var queryParams = new List<string>(); 
            if (includeAllEfforts.HasValue) queryParams.Add($"include_all_efforts={includeAllEfforts.Value.ToString().ToLower()}"); 
            if (queryParams.Count > 0) path += "?" + string.Join("&", queryParams); // Set up the HttpRequestMessage
            var request = new HttpRequestMessage(HttpMethod.Get, path); // Set the authorization header (assumes Bearer token is used)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "your_access_token"); // Send the request
            var response = await _httpClient.SendAsync(request); // Check the response status
               if (!response.IsSuccessStatusCode) throw new ApiException((int)response.StatusCode, $"Error calling GetActivityById: {response.ReasonPhrase}"); 
            var content = await response.Content.ReadAsStringAsync(); 
            return JsonConvert.DeserializeObject<DetailedActivity>(content); }
        public DetailedActivity GetActivityById(long? id, bool? includeAllEfforts)
        {

            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetActivityById");


            var path = "/activities/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));

            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;

            if (includeAllEfforts != null) queryParams.Add("include_all_efforts", ApiClient.ParameterToString(includeAllEfforts)); // query parameter

            // authentication setting, if any
            String[] authSettings = new String[] { "strava_oauth" };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling GetActivityById: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling GetActivityById: " + response.ErrorMessage, response.ErrorMessage);

            return (DetailedActivity)ApiClient.Deserialize(response.Content, typeof(DetailedActivity), response.Headers);
        }
    }
}
