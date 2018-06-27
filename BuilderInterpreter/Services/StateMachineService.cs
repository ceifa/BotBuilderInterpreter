using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    public class StateMachineService
    {
        private const string NoActionKeyword = "#noaction#";

        private readonly BotFlow _botFlow;        
        private readonly IUserContextService _userContext;
        private readonly INoAction _noAction;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSemaphoreService _userSemaphoreService;

        public StateMachineService(BotFlow botFlow, IUserContextService userContext, INoAction noAction, IServiceProvider serviceProvider, UserSemaphoreService userSemaphoreService)
        {
            _userContext = userContext;
            _botFlow = botFlow;
            _noAction = noAction;
            _serviceProvider = serviceProvider;
            _userSemaphoreService = userSemaphoreService;
        }

        public async Task<Document[]> HandleUserInput(string userIdentity, string input)
        {
            var userSemaphore = await _userSemaphoreService.GetSemaphoreByUserIdentity(userIdentity);

            try
            {
                await userSemaphore.WaitAsync();

                var userContext = await _userContext.GetUserContext(userIdentity);
                var stateId = userContext.StateId;

                var state = string.IsNullOrEmpty(stateId) ? default : _botFlow.States.SingleOrDefault(x => x.Key == stateId).Value;
                
                if (state == default)
                {
                    state = _botFlow.States.Values.Single(x => x.IsRoot);
                }

                var oldInput = state.InteractionActions.Single(x => x.Input != null).Input;
                if (!string.IsNullOrEmpty(oldInput.Variable))
                    userContext.Variables[oldInput.Variable] = input;

                userContext.Variables["input"] = input;

                var documents = new List<Document>();

                do
                {
                    var newStateId = StateMachineHelper.GetNewStateId(input, userContext.Variables, state.OutputConditions, state.DefaultOutput);
                    state = _botFlow.States.Single(x => x.Key == newStateId).Value;

                    state.InteractionActions.Where(x => x.Input == null).ForEach(async x =>
                    {
                        var content = x.Action.Message.Content;

                        var variables = userContext.Variables;
                        variables["config"] = _botFlow.GlobalVariables;
                        content = StateMachineHelper.GetDocumentWithVariablesReplaced(content, content.GetMediaType(), variables);

                        if (_noAction != null && content.ToString().StartsWith(NoActionKeyword))
                            await _noAction.ExecuteNoAction(userIdentity, content.ToString().Remove(0, NoActionKeyword.Length), userContext);
                        else
                            documents.Add(content);
                    });
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
