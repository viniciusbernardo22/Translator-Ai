using Microsoft.AspNetCore.Mvc;
using Translator_Ai.Infraestructure.Services;

namespace Translator_Ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController(ITranslatorService translator) : ControllerBase
    {
        [HttpGet("{key}/{desiredLanguage}")]
        public async Task<IActionResult> TranslatePhrase(string key, string desiredLanguage)
        {
            var response = await translator.TranslatePhraseAsync(key, desiredLanguage);

            return Ok(response);
        }
    }
}
