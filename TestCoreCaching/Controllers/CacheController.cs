using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheMemory _cacheModel;
        public CacheController(ICacheMemory cacheModel)
        {
            _cacheModel = cacheModel;
        }
        [HttpGet("GetCache")]
        public IActionResult Get(string a)
        {
            var cache = _cacheModel.Get<string>("test");
            if(cache == null)
            {
                return NotFound();
            }
            var increaCache = cache + a;
            _cacheModel.Add("test", increaCache);
            return Ok(increaCache);
        }
    }
}
