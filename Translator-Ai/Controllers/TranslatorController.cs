using Microsoft.AspNetCore.Mvc;
using Translator_Ai.Infraestructure.Models;
using Translator_Ai.Infraestructure.Services;

namespace Translator_Ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController(ITranslatorService translator) : ControllerBase
    {
        private readonly ITranslatorService _translatorService = translator;

        [HttpGet("{key}/{desiredLanguage}")]
        public async Task<IActionResult> TranslatePhrase(string key, string desiredLanguage)
        {
            var response = await _translatorService.TranslatePhraseAsync(key, desiredLanguage);

            return Ok(response);
        }

        [HttpGet("get-languages")]
        public async Task<IActionResult> GetLanguages()
        {
            var response = await _translatorService.GetLanguagesAsync();

            return Ok(response);
        }


        [HttpPost("translate-json")]
        public async Task<IActionResult> TranslateJson([FromBody] GetTranslatedJson request)
        {
            var response = await _translatorService.TranslateJsonAsync(request.JsonObject, request.Language);
            return Ok(response);
        }
    }
}
