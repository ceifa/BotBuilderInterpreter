using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Attributes;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultRedirectAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IEnumerable<INoAction> _noActions;
        private readonly IVariableService _variableService;

        public DefaultRedirectAction(IEnumerable<INoAction> noActions, IVariableService variableService)
        {
            _noActions = noActions;
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.NoAction;

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<Redirect>();

            settings = await _variableService.ReplaceVariablesInObjectAsync(settings, userContext);

            foreach (var noAction in _noActions)
            {
                var method = noAction.GetType().GetMethod(nameof(noAction.ExecuteNoAction));
                var attr = method.GetCustomAttributes(typeof(NoActionTokenAttribute), false);

                if (attr?.Length == 1)
                {
                    var token = (NoActionTokenAttribute)attr[0];

                    if (token.Token == settings.Address)
                    {
                        await (Task)method.Invoke(noAction, new object[] { settings.Context?.Value, userContext });
                    }
                }
            }
        }
    }
}