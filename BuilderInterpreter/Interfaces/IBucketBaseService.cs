using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBucketBaseService
    {
        Task<T> GetBucketObjectAsync<T>(string key);

        Task<bool> SetBucketObjectAsync<T>(string key, T document, TimeSpan expiration = default);
    }
}