using BuilderInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IUserContextService
    {
        Task<UserContext> GetUserContext(string userIdentity);

        Task<bool> SetUserContext(string userIdentity, UserContext userContext);
    }
}
