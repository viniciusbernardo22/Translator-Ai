using Microsoft.AspNetCore.Mvc;
using Translator_Ai.Infraestructure.Services;

namespace Translator_Ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        private readonly ITranslatorService _translatorService;
        public TranslatorController(ITranslatorService translator)
        {
            _translatorService = translator;
        }
        [HttpGet("{key}/{desiredLanguage}")]
        public async Task<IActionResult> TranslatePhrase(string key, string desiredLanguage)
        {
            var response = await _translatorService.TranslatePhraseAsync(key, desiredLanguage);

            return Ok(response);
        }
    }
}
