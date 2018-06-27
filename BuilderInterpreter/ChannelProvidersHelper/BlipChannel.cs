using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Services;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.ChannelProvidersHelper
{
    public class BlipChannel
    {
        private readonly StateMachineService _stateMachineService;
        private readonly IUserContextService _userContext;

        public BlipChannel(StateMachineService stateMachineService, IUserContextService userContext)
        {
            _stateMachineService = stateMachineService;
            _userContext = userContext;
        }

        public async Task MessageReceiverHelper(Message message, Func<Document, Task> sendMessageFunc)
        {
            var user = await _userContext.GetUserContext(message.From);
            user.Variables["message"] = message;
            await _userContext.SetUserContext(message.From, user);

            var documents = await _stateMachineService.HandleUserInput(message.From, message.Content.ToString());
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
