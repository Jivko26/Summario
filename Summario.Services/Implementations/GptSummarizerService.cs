using Summario.Services.Helpers;
using Summario.Services.Interfaces;
using System.Text.Json;
using System.Text;

namespace Summario.Services.Implementations
{
    public class GptSummarizerService : IGptSummarizerService
    {
        private readonly HttpClient _httpClient;
        private readonly int _maxInputLength = 4096;

        public GptSummarizerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<string>> SummarizeAsync(string text)
        {
            var chunks = CutInput(text);
            var summaries = new List<string>();

            foreach (var chunk in chunks)
            {
                var summarizedChunk = await SummarizeChunkAsync(chunk);
                summaries.Add(summarizedChunk);
            }

            return summaries;
        }

        private List<string> CutInput(string text)
        {
            var chunks = new List<string>();

            while (!string.IsNullOrEmpty(text))
            {
                string chunk = text.Substring(0, Math.Min(_maxInputLength, text.Length));
                text = text.Substring(chunk.Length);
                chunks.Add(chunk);
            }

            return chunks;
        }

        private async Task<string> SummarizeChunkAsync(string text)
        {
            var messages = new[]
        {
            new { role = "system", content = "You are a helpful summarizer" },
            new { role = "user", content = $"Summarize the following: {text}" }
        };

            var data = new
            {
                model = "gpt-3.5-turbo",
                messages
            };

            var content = JsonSerializer.Serialize(data);
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions",
                new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"The API request failed with status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseContent);

            return responseObject?.Choices[0].Message.Content ?? string.Empty;
        }
    }

}
