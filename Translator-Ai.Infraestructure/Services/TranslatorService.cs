
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Translator_Ai.Infraestructure.Configurations.Translator;
using Translator_Ai.Infraestructure.Models;

namespace Translator_Ai.Infraestructure.Services
{
    public class TranslatorService(HttpClient httpClient, IOptions<TranslatorOptions> options) : ITranslatorService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IOptions<TranslatorOptions> _options = options;

        public async Task<List<GetLanguagesResponse>> GetLanguagesAsync()
        {
            var route = "/languages?api-version=3.0&scope=translation";

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_options.Value.Endpoint}{route}")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var translationNode = doc.RootElement.GetProperty("translation");

            var result = new List<GetLanguagesResponse>();

            foreach (var language in translationNode.EnumerateObject())
            {
                var code = language.Name;
                var name = language.Value.GetProperty("name").GetString();

                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name))
                    result.Add(new GetLanguagesResponse(code, name));

            }

            return [.. result.OrderBy(l => l.Language)];
        }

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
