namespace GraphReview.Application.Abstractions.Caching
{
    public interface ICachingService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
            where T : class;

        Task CacheAsync<T>(string key, T value, CancellationToken cancellationToken = default)
            where T : class;

        Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default)
            where T : class;
    }
}
