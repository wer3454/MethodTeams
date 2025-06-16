namespace MethodologyMain.Infrastructure.Interfaces
{
    public interface IRedisService
    {
        Task<T> GetStringFromCacheAsync<T>(string key);

        Task SetStringToCacheAsync<T>(string key, T value);

        Task RemoveStringFromCacheAsync(string key);
    }
}
