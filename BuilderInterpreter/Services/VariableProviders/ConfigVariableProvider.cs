using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Services.VariableProviders
{
    internal class ConfigVariableProvider : IVariableProvider
    {
        private readonly IBotConfigurationService _botConfigurationService;

        public ConfigVariableProvider(IBotConfigurationService botConfigurationService)
        {
            _botConfigurationService = botConfigurationService;
        }

        public string VariableName => "config";

        public async Task<object> GetVariableValue(UserContext userContext)
        {
            return await _botConfigurationService.GetBotConfigurationAsync();
        }
    }
}