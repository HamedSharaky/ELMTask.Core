using System;
using System.Threading.Tasks;
using ELM.Core.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ELM.Core.Infrastructure.Common.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task CreateCacheEntryAsync<T>(string cacheKey, T cacheItem, DateTimeOffset absoluteExpiration)
        {
            if (cacheItem != null)
            {
                _cache.Set(cacheKey, cacheItem, absoluteExpiration);
            }

            return Task.CompletedTask;
        }

        public bool IsCacheEntryExist(string cacheKey)
        {
            return _cache.TryGetValue(cacheKey, out var entry);
        }

        public T GetCacheEntry<T>(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out T entry);

            return entry;
        }

        public void RemoveCacheEntry(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

    }
}