using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultProcessHttpAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IVariableService _variableService;

        public DefaultProcessHttpAction(IVariableService variableService)
        {
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.ProcessHttp;

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<ProcessHttp>();

            settings = await _variableService.ReplaceVariablesInObjectAsync(settings, userContext);
            var responseMessage = await settings.ExecuteHttpRequest();

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var responseStatusCode = responseMessage.StatusCode;

            userContext.SetVariable(settings.ResponseStatusVariable, (int) responseStatusCode);
            userContext.SetVariable(settings.ResponseBodyVariable, responseBody);
        }
    }
}