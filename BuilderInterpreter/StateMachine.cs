using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;

namespace BuilderInterpreter
{
    internal class StateMachine : IStateMachine
    {
        private readonly IBotFlowService _botFlowService;
        private readonly ICustomActionService _customActionService;
        private readonly INlpProvider _nlpProvider;
        private readonly IStateMachineService _stateMachineService;
        private readonly IUserSemaphoreService _userSemaphoreService;
        private readonly IVariableService _variableService;

        public StateMachine(IBotFlowService botFlowService,
            IUserSemaphoreService userSemaphoreService,
            IVariableService variableService,
            ICustomActionService customActionService,
            IStateMachineService stateMachineService,
            INlpProvider nlpProvider)
        {
            _botFlowService = botFlowService;
            _userSemaphoreService = userSemaphoreService;
            _variableService = variableService;
            _stateMachineService = stateMachineService;
            _customActionService = customActionService;
            _nlpProvider = nlpProvider;
        }

        public async Task<Document[]> HandleUserInput(string userIdentity, Message input, UserContext userContext, CancellationToken cancellationToken)
        {
            var userSemaphore = await _userSemaphoreService.GetSemaphoreByUserIdentity(userIdentity);

            try
            {
                await userSemaphore.WaitAsync();

                var flow = await _botFlowService.GetBotFlow();

                userContext = userContext ?? await UserContext.GetOrCreateAsync(userIdentity, cancellationToken);

                var state = _stateMachineService.GetCurrentUserState(userContext, flow);

                var oldInput = state.InteractionActions.Single(x => x.Input != default).Input;
                if (!string.IsNullOrEmpty(oldInput.Variable))
                    userContext.SetVariable(oldInput.Variable, input);

                userContext.SetVariable("input", input);
                userContext.PopulateNlpResponse(input.ToString(), _nlpProvider);

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
                await userContext.AddOrUpdateAsync(cancellationToken);

                return documents.ToArray();

            }
            finally
            {
                userSemaphore.Release();
            }
        }
    }
}