using Ground.Extensions.DependencyInjection.Sample.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Extensions.DependencyInjection.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidGeneratorController : ControllerBase
    {
        private readonly IGetGuidSingletoneService _getRandomNumberSingletoneService;
        public GuidGeneratorController(IGetGuidSingletoneService getRandomNumberSingletoneService)
        {
            _getRandomNumberSingletoneService = getRandomNumberSingletoneService;
        }

        [HttpGet("GetRandomNumberSingletone")]
        public async Task<IActionResult> GetRandomNumberSingletone([FromServices] IGetGuidSingletoneService singletoneService1,
            [FromServices] IGetGuidSingletoneService singletoneService2)
        => Ok(string.Format("Number1: {0}, Number2: {1}", singletoneService1.Execute(), singletoneService2.Execute()));
    }
}
