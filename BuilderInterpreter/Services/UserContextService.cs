using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BuilderInterpreter
{
    internal class UserContextService : IUserContextService
    {
        private const string BUCKET_KEYWORD = nameof(UserContext);

        private readonly IBucketBaseService _bucketService;
        private readonly IMemoryCache _memoryCache;

        public UserContextService(IBucketBaseService bucketService, IMemoryCache memoryCache)
        {
            _bucketService = bucketService;
            _memoryCache = memoryCache;
        }

        public Task<UserContext> GetUserContext(string userIdentity)
        {
            var bucketIdentifier = GetBucketIdentifier(userIdentity);

            return _memoryCache.GetOrCreateAsync(userIdentity, async factory =>
            {
                factory.SlidingExpiration = TimeSpan.FromMinutes(1);

                var userContext = await _bucketService.GetBucketObjectAsync<UserContext>(bucketIdentifier);

                if (userContext == default)
                    userContext = new UserContext
                    {
                        Identity = userIdentity,
                        Variables = new Dictionary<string, object>(),
                        Contact = new UserContact()
                    };

                return userContext;
            });
        }

        public async Task<bool> SetUserContext(string userIdentity, UserContext userContext)
        {
            var bucketIdentifier = GetBucketIdentifier(userIdentity);

            var bucketSaveSuccess =
                await _bucketService.SetBucketObjectAsync(bucketIdentifier, userContext, TimeSpan.FromDays(2));
            var memoryCacheSaveSuccess = _memoryCache.Set(userIdentity, userContext) != default;

            return bucketSaveSuccess && memoryCacheSaveSuccess;
        }

        private string GetBucketIdentifier(string userIdentity)
        {
            return $"{BUCKET_KEYWORD}_{userIdentity}";
        }
    }
}