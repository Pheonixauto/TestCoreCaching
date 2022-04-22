using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace TestCoreCaching.CacheServer
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        public CacheService(IMemoryCache memoryCache, IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }
        public void Add<T>(string cacheKey, T value)
        {
            var b = _configuration.GetValue<string>("RedisCacheServerUrl");
            if (b != null)
            {
                // redis
                var cacheExpriOptionRedis = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(50),
                };
                var cachedDataString = JsonConvert.SerializeObject(value);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
                _distributedCache.Set(cacheKey, newDataToCache, cacheExpriOptionRedis);
            }
            else
            {
                //memory
                var cacheExpriOptionMemory = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(50),
                };
                _memoryCache.Set(cacheKey, value, cacheExpriOptionMemory);
            }
        }
        public T Get<T>(string cacheKey)
        {
            var b = _configuration.GetValue<string>("RedisCacheServerUrl");
            if (b != null)
            {
                // redis

                var result = _distributedCache.Get(cacheKey);
                if (result != null)
                {
                    var cachedDataString = Encoding.UTF8.GetString(result);
                    var cachedDataString1 = JsonConvert.DeserializeObject<T>(cachedDataString);
                    return cachedDataString1;
                }
                return default!;
            }
            else
            {
                //memory
                var result = _memoryCache.Get(cacheKey);
                if (result != null)
                {
                    var r = JsonConvert.SerializeObject(result);
                    T re = JsonConvert.DeserializeObject<T>(r)!;
                    return re;
                }
                return default!;
            }
        }
        public void Delete(string cacheKey)
        {
            var b = _configuration.GetValue<string>("RedisCacheServerUrl");
            if (b != null)
            {
                _distributedCache.Remove(cacheKey);

            }
            else
            {
                _memoryCache.Remove(cacheKey);
            }
        }
    }
}
