using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal class BotFlowService : IBotFlowService
    {
        public Task<BotFlow> GetBotFlow()
        {
            return Task.FromResult(new BotFlow(new Dictionary<string, State>()));
        }
    }
}