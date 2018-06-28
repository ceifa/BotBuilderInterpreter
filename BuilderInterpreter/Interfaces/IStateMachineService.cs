using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    public interface IStateMachineService
    {
        State GetCurrentUserState(UserContext userContext);

        State GetNextUserState(UserContext userContext, State lastState);
    }
}