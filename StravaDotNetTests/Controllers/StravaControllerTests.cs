using System.Net;
using System.Net.Http.Json;
using Strava.Models;
using StravaDotNet.Controllers;
using Xunit;

namespace StravaDotNetTests.Controllers
{
    public class StravaControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly StravaController _controller;
        public StravaControllerTests()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new DetailedActivity
                {
                    Id = 1,
                    Distance = 5,
                    ElapsedTime = 1234
                })
            };
            var httpMessageHandler = new TestHttpMessageHandler(response);
            _httpClient = new HttpClient(httpMessageHandler);
            _controller = new StravaController(_httpClient);
        }
        [Fact]
        public async Task GetActivityByIdAsync_ReturnsExpectedActivity()
        {
            // Act
            var result = await _controller.GetActivityByIdAsync(1, false);
            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(1, result.Id);
            Xunit.Assert.Equal(5, result.Distance);
            Xunit.Assert.Equal(1234, result.ElapsedTime);
        }
    }
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;
        public TestHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
