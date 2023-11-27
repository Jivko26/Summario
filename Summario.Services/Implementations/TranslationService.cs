using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Summario.Services.Interfaces;

public class TranslationService : ITranslationService
{
    private static readonly string key = "1a60e0fb82f44ede9653ad4dc200ea5d";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";

    private static readonly string location = "westeurope";


    public async Task<string> Translate(string summary)
    {
        // Input and output languages are defined as parameters.
        string route = "/translate?api-version=3.0&from=en&to=nl";
        object[] body = new object[] { new { Text = summary } };
        var requestBody = JsonConvert.SerializeObject(body);

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);

            // Send the request and get response.
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}

