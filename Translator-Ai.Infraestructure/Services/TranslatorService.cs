
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Translator_Ai.Infraestructure.Configurations.Translator;

namespace Translator_Ai.Infraestructure.Services
{
    public class TranslatorService(HttpClient httpClient, IOptions<TranslatorOptions> options) : ITranslatorService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IOptions<TranslatorOptions> _options = options;

        public async Task<string> TranslatePhraseAsync(string key, string desiredLanguage)
        {
            var route = $"/translate?api-version=3.0&to={desiredLanguage}";

            var body = new object[] { new { Text = key } };
            var requestBody = JsonSerializer.Serialize(body);

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_options.Value.Endpoint}{route}"),
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("Ocp-Apim-Subscription-Key", _options.Value.Key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", "brazilsouth");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var translation = doc.RootElement[0].GetProperty("translations")[0].GetProperty("text").GetString();

            return translation ?? key;
        }
    }
}
