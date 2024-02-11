using System;
using System.Threading.Tasks;

namespace ELM.Core.Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task CreateCacheEntryAsync<T>(string cacheKey, T cacheItem, DateTimeOffset absoluteExpiration);
        bool IsCacheEntryExist(string cacheKey);
        T GetCacheEntry<T>(string cacheKey);
        void RemoveCacheEntry(string cacheKey);
    }
}
