﻿using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    internal class StateMachine : IStateMachine
    {
        private readonly IBotFlowService _botFlowService;
        private readonly IUserContextService _userContext;
        private readonly IUserSemaphoreService _userSemaphoreService;
        private readonly IVariableService _variableService;
        private readonly IStateMachineService _stateMachineService;
        private readonly ICustomActionService _customActionService;
        private readonly INlpProvider _nlpProvider;

        public StateMachine(IBotFlowService botFlowService,
            IUserContextService userContext,
            IUserSemaphoreService userSemaphoreService,
            IVariableService variableService,
            ICustomActionService customActionService,
            IStateMachineService stateMachineService,
            INlpProvider nlpProvider)
        {
            _botFlowService = botFlowService;
            _userContext = userContext;
            _userSemaphoreService = userSemaphoreService;
            _variableService = variableService;
            _stateMachineService = stateMachineService;
            _customActionService = customActionService;
            _nlpProvider = nlpProvider;
        }

        public async Task<Document[]> HandleUserInput(string userIdentity, string input, UserContext userContext)
        {
            var userSemaphore = await _userSemaphoreService.GetSemaphoreByUserIdentity(userIdentity);

            try
            {
                await userSemaphore.WaitAsync();

                var flow = await _botFlowService.GetBotFlow();

                userContext = userContext ?? await _userContext.GetUserContext(userIdentity);

                var state = _stateMachineService.GetCurrentUserState(userContext, flow);

                var oldInput = state.InteractionActions.Single(x => x.Input != default).Input;
                if (!string.IsNullOrEmpty(oldInput.Variable))
                    userContext.SetVariable(oldInput.Variable, input);

                userContext.SetVariable("input", input);
                userContext.PopulateNlpResponse(input, _nlpProvider);

                var documents = new List<Document>();

                do
                {
                    state = await _stateMachineService.GetNextUserStateAsync(userContext, state, flow);

                    await _customActionService.ExecuteCustomActions(state.EnteringCustomActions, userContext);

                    state.InteractionActions.Where(x => x.Input == default).ForEach(async x =>
                    {
                        var content = x.Action.Message.Content;

                        content = await _variableService.ReplaceVariablesInDocumentAsync(content, userContext);

                        documents.Add(content);
                    });

                    await _customActionService.ExecuteCustomActions(state.LeavingCustomActions, userContext);
                } while (!state.InteractionActions.Any(x => x.Input?.Bypass == false));

                userContext.StateId = state.Id;
                await _userContext.SetUserContext(userIdentity, userContext);

                return documents.ToArray();
            }
            finally
            {
                userSemaphore.Release();
            }
        }
    }
}
