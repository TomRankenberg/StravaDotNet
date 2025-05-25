using Newtonsoft.Json;
using StravaDotNet.ViewModels;
using System.Text;

namespace StravaDotNet.Components.Services
{
    public class PythonService
    {
        private readonly HttpClient _httpClient;

        public PythonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PredictAsync(List<ActivityVm> activities)
        {// Doesnt work yet, not implemented
            var json = JsonConvert.SerializeObject(activities);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/predict", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
