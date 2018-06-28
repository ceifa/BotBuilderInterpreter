using BuilderInterpreter.Interfaces;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.ChannelProvidersHelper
{
    public class BlipChannel
    {
        private readonly IStateMachine _stateMachineService;
        private readonly IUserContextService _userContext;
        private readonly IVariableService _variableService;

        public BlipChannel(Interfaces.IStateMachine stateMachineService, IUserContextService userContext, IVariableService variableService)
        {
            _stateMachineService = stateMachineService;
            _userContext = userContext;
            _variableService = variableService;
        }

        public async Task MessageReceiverHelper(Message message, Func<Document, Task> sendMessageFunc)
        {
            var user = await _userContext.GetUserContext(message.From);
            _variableService.AddOrUpdate("message", message, user.Variables);

            var documents = await _stateMachineService.HandleUserInput(message.From, message.Content.ToString(), user);
            var messages = new List<DocumentContainer>();

            foreach (var document in documents)
            {
                if (document.GetMediaType() == "application/vnd.lime.chatstate+json")
                {
                    object interval = null;

                    if (messages.Count > 0)
                    {
                        await sendMessageFunc(new DocumentCollection
                        {
                            ItemType = DocumentContainer.MediaType,
                            Items = messages.ToArray()
                        });

                        messages.Clear();
                    }

                    if ((document as JsonDocument)?.TryGetValue("interval", out interval) == true)
                    {
                        await sendMessageFunc(document);
                        await Task.Delay(Convert.ToInt32(interval));
                    }
                }
                else
                {
                    messages.Add(new DocumentContainer
                    {
                        Value = document
                    });
                }
            }

            if (messages.Count > 0)
            {
                await sendMessageFunc(new DocumentCollection
                {
                    ItemType = DocumentContainer.MediaType,
                    Items = messages.ToArray()
                });
            }
        }
    }
}
