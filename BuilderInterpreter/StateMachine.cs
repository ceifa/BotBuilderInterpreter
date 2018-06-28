using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class StateMachine : IStateMachine
    {
        private const string NoActionKeyword = "#noaction#";

        private readonly BotFlow _botFlow;        
        private readonly IUserContextService _userContext;
        private readonly INoAction _noAction;
        private readonly IUserSemaphoreService _userSemaphoreService;
        private readonly IVariableService _variableService;
        private readonly IStateMachineService _stateMachineService;
        private readonly ICustomActionService _customActionService;

        public StateMachine(BotFlow botFlow, 
            IUserContextService userContext, 
            INoAction noAction,
            IUserSemaphoreService userSemaphoreService,
            IVariableService variableService,
            ICustomActionService customActionService,
            IStateMachineService stateMachineService)
        {
            _userContext = userContext;
            _botFlow = botFlow;
            _noAction = noAction;
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

                var state = _stateMachineService.GetCurrentUserState(userContext);

                var oldInput = state.InteractionActions.Single(x => x.Input != null).Input;
                if (!string.IsNullOrEmpty(oldInput.Variable))
                    _variableService.AddOrUpdate(oldInput.Variable, input, userContext.Variables);

                _variableService.AddOrUpdate("input", input, userContext.Variables);

                var documents = new List<Document>();

                do
                {
                    state = _stateMachineService.GetNextUserState(userContext, state);

                    await _customActionService.ExecuteCustomActions(state.EnteringCustomActions, userContext);

                    state.InteractionActions.Where(x => x.Input == null).ForEach(async x =>
                    {
                        var content = x.Action.Message.Content;
                        
                        content = _variableService.ReplaceVariablesInDocument(content, userContext.Variables);

                        if (_noAction != null && input.StartsWith(NoActionKeyword))
                            await _noAction.ExecuteNoAction(userIdentity, content.ToString().Remove(0, NoActionKeyword.Length), userContext);
                        else
                            documents.Add(content);
                    });

                    await _customActionService.ExecuteCustomActions(state.LeavingCustomActions, userContext);
                } while (!state.InteractionActions.Any(x => x.Input != null && !x.Input.Bypass));

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
