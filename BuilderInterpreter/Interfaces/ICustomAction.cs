using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomAction
    {
        CustomActionType ActionType { get; }

        Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload);
    }
}
