namespace Translator_Ai.Infraestructure.Services
{
    public interface ITranslatorService
    {
        public Task<string> TranslatePhraseAsync(string key, string desiredLanguage);
    }
}
