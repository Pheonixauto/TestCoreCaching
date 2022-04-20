using Microsoft.Extensions.Caching.Memory;

namespace TestCoreCaching
{
    public static class CacheModel<T> where T : class
    {
        private static IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        public static void Add(T cacheKey, int value)
        {
            var cacheExpriOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(3),
            };

            _memoryCache.Set(cacheKey,value, cacheExpriOption);
        }
        public static int Get(T cacheKey)
        {
            var cacheExpriOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(3),
            };
            var result = _memoryCache.Get(cacheKey);
            var r= Convert.ToInt32(result)+1;
            _memoryCache.Set(cacheKey, r, cacheExpriOption);
            return r;
        }
        public static void Delete(T cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }    

    }
 
}
