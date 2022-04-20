using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        [HttpGet("sum")]
        public void Sum()
        {
            int x = 0;
            int y = 1;
            CacheModel.Add("sum", x + y);
        }
    }
}
