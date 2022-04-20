using Microsoft.Extensions.Caching.Memory;

namespace TestCoreCaching
{
    public static class CacheModel
    {
        private static IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        public static void Add(string cacheKey, int value)
        {
            var cacheExpriOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20),
            };

            _memoryCache.Set(cacheKey,value);
        }
        public static int Get(string cacheKey)
        {
            var result = _memoryCache.Get(cacheKey);
            var r= Convert.ToInt32(result)+1;
            _memoryCache.Set(cacheKey, r);
            return Convert.ToInt32(_memoryCache.Get(cacheKey));
        }
        public static void Delete(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);

        }    

    }
 
}
