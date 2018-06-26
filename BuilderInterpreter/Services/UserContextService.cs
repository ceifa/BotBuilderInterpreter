using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    class UserContextService : IUserContextService
    {
        private readonly BucketBaseService _bucketService;

        public UserContextService(BucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<UserContext> GetUserContext(string userIdentity)
        {
            userIdentity = Uri.EscapeDataString(userIdentity);
            var userContext = await _bucketService.GetBucketObjectAsync<UserContext>(userIdentity);

            if (userContext == default(UserContext))
                userContext = new UserContext
                {
                    Identity = userIdentity,
                    Variables = new Dictionary<string, object>(),
                    FirstInteraction = true
                };

            return userContext;
        }

        public Task<bool> SetUserContext(string userIdentity, UserContext userContext)
        {
            userIdentity = Uri.EscapeDataString(userIdentity);
            return _bucketService.SetBucketObjectAsync(userIdentity, userContext, TimeSpan.FromDays(2));
        }
    }
}
