using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface IUserContextService
    {
        Task<UserContext> GetUserContext(string userIdentity);

        Task<bool> SetUserContext(string userIdentity, UserContext userContext);
    }
}