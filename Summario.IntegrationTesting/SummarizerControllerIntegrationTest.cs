
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Summario.IntegrationTesting
{
    public class SummarizerControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SummarizerControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SummarizeArticle_ReturnsSummaries()
        {
            // Arrange
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(File.OpenRead("path_to_test_pdf.pdf"));
            content.Add(fileContent, "file", "test.pdf");

            // Act
            var response = await _client.PostAsync("/api/summarizer/SummarizeArtilce", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(responseContent);
        }
    }

}
