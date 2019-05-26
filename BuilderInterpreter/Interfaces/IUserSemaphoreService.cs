using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IUserSemaphoreService
    {
        Task<SemaphoreSlim> GetSemaphoreByUserIdentity(string identity);
    }
}