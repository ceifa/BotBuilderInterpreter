using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomActionService
    {
        void ExecuteCustomActions(CustomAction[] customActions, UserContext userContext);
    }
}
