namespace TestCoreCaching
{
    public interface ICacheMemory
    {
        void Add<T>(string cacheKey, T value);
        void Delete(string cacheKey);
        T Get<T>(string cacheKey);
    }
}