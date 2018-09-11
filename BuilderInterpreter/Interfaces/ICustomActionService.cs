using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface ICustomActionService
    {
        Task ExecuteCustomActions(CustomAction[] customActions, UserContext userContext);
    }
}
