using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    internal interface ICustomActionService
    {
        Task ExecuteCustomActions(CustomAction[] customActions, UserContext userContext);
    }
}