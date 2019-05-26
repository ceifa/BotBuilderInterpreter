using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using Lime.Protocol;

namespace BuilderInterpreter.ChannelProvidersHelper
{
    public class BlipChannel
    {
        private readonly IStateMachine _stateMachineService;
        private readonly IUserContextService _userContext;

        public BlipChannel(IStateMachine stateMachineService, IUserContextService userContext)
        {
            _stateMachineService = stateMachineService;
            _userContext = userContext;
        }

        public async Task MessageReceiverHelper(Message message, Func<Document, Task> sendMessageFunc)
        {
            var user = await _userContext.GetUserContext(message.From);
            user.SetVariable(nameof(message), message);

            var documents = await _stateMachineService.HandleUserInput(message.From, message.Content.ToString(), user);
            var messages = new List<DocumentContainer>();

            foreach (var document in documents)
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

                    if ((document as JsonDocument)?.TryGetValue(nameof(interval), out interval) == true)
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

            if (messages.Count > 0)
                await sendMessageFunc(new DocumentCollection
                {
                    ItemType = DocumentContainer.MediaType,
                    Items = messages.ToArray()
                });
        }
    }
}