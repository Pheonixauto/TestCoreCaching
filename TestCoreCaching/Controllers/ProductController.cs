using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using TestCoreCaching.DistributedCache;
using TestCoreCaching.Models;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDistributedCacheService distributedCacheService;

        public ProductController(IDistributedCacheService distributedCacheService)
        {
            this.distributedCacheService = distributedCacheService;
        }
        [HttpGet]
        public IActionResult GetProduct()
        {
            string product = "car";
            return Ok(product);
        }
        [HttpGet("cache")]
        public  IActionResult GetCache()
        {
            var cacheKey = "test";
            string product = "car";

            var cacheData = distributedCacheService.Get<string>(cacheKey);
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            else
            {
                distributedCacheService.Add(cacheKey, product);

            }
            return Ok("add cache");
        }
    }
}
