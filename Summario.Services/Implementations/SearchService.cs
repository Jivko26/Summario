using Summario.Services.Interfaces;

public class SearchService : ISearchService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "8aed44f17979403d8c817339c367093f"; 

    public SearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SearchAsync(string query)
    {
        var requestUri = $"https://api.bing.microsoft.com/v7.0/search?q={Uri.EscapeDataString(query)}";
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
