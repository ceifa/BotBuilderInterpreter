using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;

namespace BuilderInterpreter.Services
{
    internal class BotConfigurationService : IBotConfigurationService
    {
        public Task<Dictionary<string, string>> GetBotConfigurationAsync()
        {
            return Task.FromResult(new Dictionary<string, string>());
        }
    }
}