using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheModel;
        public CacheController(ICacheService cacheModel)
        {
            _cacheModel = cacheModel;
        }
        [HttpGet("GetCache")]
        public int Get()
        {
            var cache = _cacheModel.Get<string>("test");
            var increaCache = cache + 1;
            _cacheModel.Add("test", increaCache);
            return increaCache;
        }
    }
}
