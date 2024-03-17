using Ground.Extensions.Translations.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Extensions.Translations.Trani.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraniController : ControllerBase
    {
        private readonly ITranslator _translator;

        public TraniController(ITranslator translator)
        {
            _translator = translator;
        }

        [HttpGet(Name = "GetTranslation")]
        public IActionResult Get(string key)
        {
            return Ok(_translator.GetString(key));
        }
    }
}
