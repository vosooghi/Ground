using Ground.Extensions.Caching.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Extensions.Caching.InMemory.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheAdapter _cacheAdapter;
        public CacheController(ICacheAdapter cacheAdapter)
        {
            _cacheAdapter = cacheAdapter;
        }
        [HttpGet("Add")]
        public IActionResult Add(string key, string value) {
            _cacheAdapter.Add(key, value,null,null);
            return Ok();
        }
        [HttpPost("Get")]
        public IActionResult Get(string key)
        {
            return Ok(_cacheAdapter.Get<string>(key));
        }
        [HttpPost("Delete")]
        public IActionResult Delete(string key)
        {
            _cacheAdapter.RemoveCache(key);
            return Ok();
        }
    }
}
