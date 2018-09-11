using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal interface IStateMachineService
    {
        State GetCurrentUserState(UserContext userContext);

        State GetNextUserState(UserContext userContext, State lastState);
    }
}