namespace TestCoreCaching.CacheServer
{
    public interface ICacheService
    {
        void Add<T>(string cacheKey, T value);
        void Delete(string cacheKey);
        T Get<T>(string cacheKey);
    }
}