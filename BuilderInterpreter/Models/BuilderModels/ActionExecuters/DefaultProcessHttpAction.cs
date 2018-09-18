using System.Threading.Tasks;
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

            settings = _variableService.ReplaceVariablesInObject(settings, userContext.Variables);
            var responseMessage = await settings.ExecuteHttpRequest();

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var responseStatusCode = responseMessage.StatusCode;

            _variableService.AddOrUpdate(settings.ResponseStatusVariable, (int)responseStatusCode, userContext.Variables);
            _variableService.AddOrUpdate(settings.ResponseBodyVariable, responseBody, userContext.Variables);
        }
    }
}
