using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    internal interface ICustomActionService
    {
        Task ExecuteCustomActions(IEnumerable<CustomAction> customActions, UserContext userContext);
    }
}