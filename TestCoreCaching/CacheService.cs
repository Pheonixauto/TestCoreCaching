using Microsoft.Extensions.Caching.Memory;

namespace TestCoreCaching
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache= memoryCache;
        }
        public void Add<T>(string cacheKey, T value)
        {
            var cacheExpriOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(15),
            };
            _memoryCache.Set(cacheKey, value,cacheExpriOption);
        }
        public int Get<T>(string cacheKey)
        {
            var result = _memoryCache.Get(cacheKey);
            return Convert.ToInt32(result);
        }
        public void Delete<T>(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

    }

}
