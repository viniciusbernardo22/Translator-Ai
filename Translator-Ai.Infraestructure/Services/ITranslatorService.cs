using Translator_Ai.Infraestructure.Models;

namespace Translator_Ai.Infraestructure.Services
{
    public interface ITranslatorService
    {
        Task<string> TranslatePhraseAsync(string key, string desiredLanguage);
        Task<List<GetLanguagesResponse>> GetLanguagesAsync();
        Task<string> TranslateJsonAsync(object jsonObject, string desiredLanguage);

    }
}
