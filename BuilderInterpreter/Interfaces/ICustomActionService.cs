using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomActionService
    {
        Task ExecuteCustomActions(CustomAction[] customActions, UserContext userContext);
    }
}
