﻿using System;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultManageListAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IDistributionListService _distributionListService;
        private readonly IVariableService _variableService;

        public DefaultManageListAction(IDistributionListService distributionListService,
            IVariableService variableService)
        {
            _distributionListService = distributionListService;
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.ManageList;

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<ManageList>();

            var listNameReplaced = await _variableService.ReplaceVariablesInStringAsync(settings.ListName, userContext);

            switch (settings.Action)
            {
                case ManageListAction.Add:
                    await _distributionListService.AddMemberOrCreateList(listNameReplaced, userContext.Identity);
                    break;

                case ManageListAction.Remove:
                    await _distributionListService.RemoveMemberFromList(listNameReplaced, userContext.Identity);
                    break;

                default:
                    throw new NotImplementedException(nameof(settings.Action));
            }
        }
    }
}