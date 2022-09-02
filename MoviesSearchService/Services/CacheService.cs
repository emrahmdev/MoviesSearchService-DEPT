using Microsoft.Extensions.Caching.Distributed;
using MoviesSearchService.Extensions;

namespace MoviesSearchService.Services
{
    public class CacheService
    {
        private readonly IDistributedCache _distributedCache;

        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

            _distributedCacheEntryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _distributedCache.GetAsync(key);

            if (value != null)
            {
                return ExtendedSerializerExtensions.Deserialize<T>(value);
            }

            return default;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var jsonByteArray = ExtendedSerializerExtensions.Serialize(value);
            await _distributedCache.SetAsync(key, jsonByteArray, _distributedCacheEntryOptions);
        }
    }
}
