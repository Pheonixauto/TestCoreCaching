using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        [HttpGet("GetCache")]
        public int Get()
        {
            return CacheModel<object>.Get(1);
        }
    }
}
