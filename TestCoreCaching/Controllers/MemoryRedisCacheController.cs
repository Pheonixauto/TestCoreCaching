using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCoreCaching.CacheServer;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryRedisCacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public MemoryRedisCacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("memoryredis")]
        public IActionResult GetCache()
        {
            var cacheKey = "test";
            string product = "car";
            var cacheData = _cacheService.Get<string>(cacheKey);
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            else
            {
                _cacheService.Add(cacheKey, product);
            }
            return Ok("add cache");
        }
    }
}
