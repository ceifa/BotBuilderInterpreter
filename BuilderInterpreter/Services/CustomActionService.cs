﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Services
{
    internal class CustomActionService : ICustomActionService
    {
        private readonly IEnumerable<ICustomAction> _customActions;

        public CustomActionService(IEnumerable<ICustomAction> customActions)
        {
            _customActions = customActions;
        }

        public async Task ExecuteCustomActions(IEnumerable<CustomAction> customActions, UserContext userContext)
        {
            foreach (var customAction in customActions)
            {
                var possibleActions = _customActions.Where(c => c.ActionType == customAction.Type);

                if (possibleActions.Count() > 1)
                    possibleActions = possibleActions.Where(p => !(p is IDefaultCustomAction));

                foreach (var action in possibleActions)
                    await action.ExecuteActionAsync(userContext, customAction.Settings);
            }
        }
    }
}