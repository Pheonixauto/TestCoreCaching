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
            //string product = "car";
            int a = 1;
            var cacheData = _cacheService.Get<int>(cacheKey);
            if (cacheData != 0)
            {
                return Ok(cacheData);
            }
            else
            {
                _cacheService.Add(cacheKey, a);
            }
            return Ok("add cache");
        }
    }
}
