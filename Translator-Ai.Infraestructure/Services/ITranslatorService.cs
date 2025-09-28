using Translator_Ai.Infraestructure.Models;

namespace Translator_Ai.Infraestructure.Services
{
    public interface ITranslatorService
    {
        public Task<string> TranslatePhraseAsync(string key, string desiredLanguage);
        public Task<List<GetLanguagesResponse>> GetLanguagesAsync();

    }
}
