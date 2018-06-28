using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IUserSemaphoreService
    {
        Task<SemaphoreSlim> GetSemaphoreByUserIdentity(string identity);
    }
}
