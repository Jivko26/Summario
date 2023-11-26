using Summario.Services.Helpers;
using Summario.Services.Interfaces;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Summario.Services.Implementations
{
    public class GptSummarizerService : IGptSummarizerService
    {
        private readonly string _apiKey = "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";
        private readonly HttpClient _httpClient;
        private readonly int _maxInputLength = 4096;

        public GptSummarizerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
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
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful summarizer" },
                new { role = "user", content = $"Summarize the following: {text}" }
            }
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return result.choices[0].message.content;
        }
    }

}
