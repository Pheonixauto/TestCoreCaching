using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        private readonly ICacheService _cacheModel;
        public SumController(ICacheService cacheModel)
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
