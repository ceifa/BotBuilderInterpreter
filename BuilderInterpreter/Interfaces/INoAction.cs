using BuilderInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface INoAction
    {
        Task ExecuteNoAction(string userIdentity, string value, UserContext userContext);
    }
}
