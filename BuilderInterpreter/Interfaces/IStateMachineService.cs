using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal interface IStateMachineService
    {
        State GetCurrentUserState(UserContext userContext, BotFlow botFlow);

        State GetNextUserState(UserContext userContext, State lastState, BotFlow botFlow);
    }
}