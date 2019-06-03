using System;
using System.Threading;
using System.Threading.Tasks;
using BuilderInterpreter.Helper;

namespace BuilderInterpreter.Services
{
    public abstract class StorageData<T> where T : StorageData<T>, new()
    {
        public string Id { get; set; }

        Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            return StorageHelper.Storage.RemoveAsync(GetKey(Id), cancellationToken);
        }

        Task AddOrUpdateAsync(CancellationToken cancellationToken = default)
        {
            return StorageHelper.Storage.AddOrUpdateAsync(GetKey(Id), cancellationToken);
        }

        async static Task<T> GetOrCreateAsync(string id, CancellationToken cancellationToken = default)
        {
            var storageKey = GetKey(id);
            var exists = await ExistsAsync(id, cancellationToken);

            if (exists)
            {
                return await StorageHelper.Storage.GetValueOrDefaultAsync<T>(id, cancellationToken);
            }

            var value = new T
            {
                Id = id
            };

            await value.AddOrUpdateAsync();

            return value;
        }

        static Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return StorageHelper.Storage.ContainsKeyAsync(GetKey(id), cancellationToken);
        }

        private static string GetKey(string id)
        {
            var type = typeof(T);
            var escapedId = Uri.EscapeDataString(id);
            return $"{type.Name}:{type.GetHashCode()}:{escapedId}";
        }
    }
}
