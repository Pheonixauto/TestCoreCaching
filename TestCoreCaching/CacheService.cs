using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestCoreCaching
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Add<T>(string cacheKey, T value)
        {
            var cacheExpriOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(9),
            };
            _memoryCache.Set(cacheKey, value, cacheExpriOption);
        }
        public T Get<T>(string cacheKey)
        {
            var result = _memoryCache.Get(cacheKey);
            if (result != null)
            {
                var r = JsonSerializer.Serialize(result);
                T re = JsonSerializer.Deserialize<T>(r)!;
                return re;
            }
            var not = "null";
            return  JsonSerializer.Deserialize<T>(not)!;
        }
        public void Delete(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

    }

}
