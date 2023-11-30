
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Summario.IntegrationTesting
{
    public class TranslationControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TranslationControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TranslateEndpoint_ReturnsTranslation()
        {
            // Arrange
            var summary = "Test summary";

            // Act
            var response = await _client.GetAsync($"/api/translation?summary={summary}");
            response.EnsureSuccessStatusCode();
            var translatedText = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(translatedText);
        }
    }

}
