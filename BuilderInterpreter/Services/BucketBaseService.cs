using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    public class BucketBaseService : BucketService
    {
        public BucketBaseService(BlipService blipService) : base(blipService)
        {
        }

        public async Task<T> GetBucketObjectAsync<T>(string key)
        {
            var json = await GetBucketAsync(key);
            if (string.IsNullOrEmpty(json)) return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public Task<bool> SetBucketObjectAsync<T>(string key, T document, TimeSpan expiration = default(TimeSpan))
        {
            return SetBucketAsync(key, document, expiration);
        }
    }
}
