using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBotConfigurationService
    {
        Task<Dictionary<string, string>> GetBotConfigurationAsync();
    }
}