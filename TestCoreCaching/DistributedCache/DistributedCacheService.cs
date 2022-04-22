
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace TestCoreCaching.DistributedCache
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache distributedCache;

        public DistributedCacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public  void Add<T>(string cacheKey, T value)
        {
            var cacheExpriOption = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                SlidingExpiration = TimeSpan.FromSeconds(15),
            };
            var cachedDataString = JsonConvert.SerializeObject(value);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
            distributedCache.Set(cacheKey, newDataToCache, cacheExpriOption);
        }
        public T Get<T>(string cacheKey)
        {
            var result = distributedCache.Get(cacheKey);
            if (result != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(result);
                var cachedDataString1 = JsonConvert.DeserializeObject<T>(cachedDataString);
                return cachedDataString1;
            }
            return default!;

        }
        public void Delete(string cacheKey)
        {
            distributedCache.Remove(cacheKey);
        }
    }
}
