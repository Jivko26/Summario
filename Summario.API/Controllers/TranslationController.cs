
using Microsoft.AspNetCore.Mvc;
using Summario.Services.Interfaces;

namespace Summario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string summary)
        {
            var result = await _translationService.Translate(summary);
            return Ok(result);
        }
    }
}
