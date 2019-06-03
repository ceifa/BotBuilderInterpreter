using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IMapStorage
    {
        Task AddOrUpdateAsync<T>(string key, T value, CancellationToken cancellationToken = default);

        Task<T> GetValueOrDefaultAsync<T>(string key, CancellationToken cancellationToken = default);

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        Task<bool> ContainsKeyAsync(string key, CancellationToken cancellationToken = default);
    }
}
