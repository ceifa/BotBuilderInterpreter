using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    interface IBucketService
    {
        Task<string> GetBucketAsync(string key);
        Task<bool> SetBucketAsync(string key, object document, TimeSpan expiration = default);
        Task<bool> DeleteBucketAsync(string key);
    }
}
