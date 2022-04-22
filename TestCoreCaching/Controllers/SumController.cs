using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        private readonly ICacheMemory _cacheModel;
        public SumController(ICacheMemory cacheModel)
        {
            _cacheModel = cacheModel;
        }
        [HttpGet("sum")]
        public void Sum(string a)
        {          
                _cacheModel.Add("test", a);          
        }
    }
}
