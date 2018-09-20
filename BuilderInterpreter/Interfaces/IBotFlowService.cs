﻿using BuilderInterpreter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBotFlowService
    {
        Task<BotFlow> GetBotFlow();
    }
}