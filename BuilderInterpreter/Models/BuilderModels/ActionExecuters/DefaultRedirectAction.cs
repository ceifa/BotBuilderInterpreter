﻿using BuilderInterpreter.Attributes;
using BuilderInterpreter.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public CustomActionType ActionType => CustomActionType.Redirect;

        public async Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload)
        {
            var settings = payload as Redirect;

            settings = _variableService.ReplaceVariablesInObject(settings, userContext.Variables);

            foreach (var noAction in _noActions)
            {
                var method = noAction.GetType().GetMethod(nameof(noAction.ExecuteNoAction));
                var attr = method.GetCustomAttributes(typeof(NoActionTokenAttribute), false);

                if (attr?.Length == 1)
                {
                    var token = (NoActionTokenAttribute)attr[0];

                    if (token.Token == settings.Address)
                    {
                        try
                        {
                            await(Task)method.Invoke(noAction, new object[] { settings.Context?.Value, userContext });
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
}