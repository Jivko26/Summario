using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Summario.IntegrationTesting
{
    public class SearchControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SearchControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SearchEndpoint_ReturnsExpectedResult()
        {
            // Arrange
            var requestUri = $"https://api.bing.microsoft.com/v7.0/search?q={Uri.EscapeDataString("test")}";
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "testapikey");

            // Act
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(content);
        }
    }

}