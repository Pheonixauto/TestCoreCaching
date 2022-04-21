namespace TestCoreCaching
{
    public interface ICacheService
    {
        void Add<T>(string cacheKey, T value);
        void Delete<T>(string cacheKey);
        int Get<T>(string cacheKey);
    }
}