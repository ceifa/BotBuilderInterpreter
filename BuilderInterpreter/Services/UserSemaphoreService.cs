using System;
using System.Threading;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BuilderInterpreter
{
    internal class UserSemaphoreService : IUserSemaphoreService
    {
        private const string MEMORY_CACHE_KEYWORD = "USER_SEMAPHORE";

        private readonly IMemoryCache _memoryCache;

        public UserSemaphoreService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<SemaphoreSlim> GetSemaphoreByUserIdentity(string identity)
        {
            return _memoryCache.GetOrCreateAsync($"{MEMORY_CACHE_KEYWORD}_{identity}", factory =>
            {
                factory.SlidingExpiration = TimeSpan.FromMinutes(1);
                return Task.FromResult(new SemaphoreSlim(1, 1));
            });
        }
    }
}