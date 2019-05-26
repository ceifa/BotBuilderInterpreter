using System;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json;

namespace BuilderInterpreter
{
    public class BucketBaseService : BucketService, IBucketBaseService
    {
        public BucketBaseService(IBlipService blipService) : base(blipService)
        {
        }

        public async Task<T> GetBucketObjectAsync<T>(string key)
        {
            var json = await GetBucketAsync(key);

            if (string.IsNullOrEmpty(json))
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public Task<bool> SetBucketObjectAsync<T>(string key, T document, TimeSpan expiration = default)
        {
            return SetBucketAsync(key, document, expiration);
        }

        public Task<bool> DeleteBucketObjectAsync(string key)
        {
            return DeleteBucketAsync(key);
        }
    }
}