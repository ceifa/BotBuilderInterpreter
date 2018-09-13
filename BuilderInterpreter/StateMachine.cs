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
        private readonly BotFlow _botFlow;
        private readonly IUserContextService _userContext;
        private readonly IUserSemaphoreService _userSemaphoreService;
        private readonly IVariableService _variableService;
        private readonly IStateMachineService _stateMachineService;
        private readonly ICustomActionService _customActionService;

        public StateMachine(BotFlow botFlow,
            IUserContextService userContext,
            IUserSemaphoreService userSemaphoreService,
            IVariableService variableService,
            ICustomActionService customActionService,
            IStateMachineService stateMachineService)
        {
            _userContext = userContext;
            _botFlow = botFlow;
            _userSemaphoreService = userSemaphoreService;
            _variableService = variableService;
            _stateMachineService = stateMachineService;
            _customActionService = customActionService;
        }

        public async Task<Document[]> HandleUserInput(string userIdentity, string input, UserContext userContext)
        {
            var userSemaphore = await _userSemaphoreService.GetSemaphoreByUserIdentity(userIdentity);

            try
            {
                await userSemaphore.WaitAsync();

                userContext = userContext ?? await _userContext.GetUserContext(userIdentity);

                _variableService.AddOrUpdate("config", _botFlow.GlobalVariables, userContext.Variables);
                _variableService.AddOrUpdate("contact", userContext.Contact, userContext.Variables);

                var state = _stateMachineService.GetCurrentUserState(userContext);

                var oldInput = state.InteractionActions.Single(x => x.Input != default).Input;
                if (!string.IsNullOrEmpty(oldInput.Variable))
                    _variableService.AddOrUpdate(oldInput.Variable, input, userContext.Variables);

                _variableService.AddOrUpdate("input", input, userContext.Variables);

                var documents = new List<Document>();

                do
                {
                    state = _stateMachineService.GetNextUserState(userContext, state);

                    await _customActionService.ExecuteCustomActions(state.EnteringCustomActions, userContext);

                    state.InteractionActions.Where(x => x.Input == default).ForEach(x =>
                    {
                        var content = x.Action.Message.Content;

                        content = _variableService.ReplaceVariablesInDocument(content, userContext.Variables);

                        documents.Add(content);
                    });

                    await _customActionService.ExecuteCustomActions(state.LeavingCustomActions, userContext);
                } while (!state.InteractionActions.Any(x => x.Input?.Bypass == false));

                userContext.FirstInteraction = false;
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
