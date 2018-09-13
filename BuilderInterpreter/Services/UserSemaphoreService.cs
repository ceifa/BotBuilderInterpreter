﻿using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    internal class UserSemaphoreService : IUserSemaphoreService
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
                factory.SlidingExpiration = TimeSpan.FromMinutes(1);
                return Task.FromResult(new SemaphoreSlim(1, 1));
            });
        }
    }
}
