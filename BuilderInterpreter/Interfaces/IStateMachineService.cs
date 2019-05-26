﻿using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal interface IStateMachineService
    {
        State GetCurrentUserState(UserContext userContext, BotFlow botFlow);

        Task<State> GetNextUserStateAsync(UserContext userContext, State lastState, BotFlow botFlow);
    }
}