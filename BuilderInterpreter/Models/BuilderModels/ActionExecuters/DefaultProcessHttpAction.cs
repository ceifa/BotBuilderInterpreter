using System.Threading.Tasks;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;

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

        public async Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload)
        {
            var settings = payload as ProcessHttp;

            settings = _variableService.ReplaceVariablesInObject(settings, userContext.Variables);
            var responseMessage = await settings.ExecuteHttpRequest();

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var responseStatusCode = responseMessage.StatusCode;

            _variableService.AddOrUpdate(settings.ResponseStatusVariable, (int)responseStatusCode, userContext.Variables);
            _variableService.AddOrUpdate(settings.ResponseBodyVariable, responseBody, userContext.Variables);
        }
    }
}
