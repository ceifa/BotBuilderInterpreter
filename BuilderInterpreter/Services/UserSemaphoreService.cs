using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    public class UserSemaphoreService
    {
        private readonly IMemoryCache _memoryCache;

        public UserSemaphoreService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<SemaphoreSlim> GetSemaphoreByUserIdentity(string identity)
        {
            return _memoryCache.GetOrCreateAsync(identity, factory => 
            {
                factory.SlidingExpiration = TimeSpan.FromMinutes(30);
                return Task.FromResult(new SemaphoreSlim(1));
            });
        }
    }
}
