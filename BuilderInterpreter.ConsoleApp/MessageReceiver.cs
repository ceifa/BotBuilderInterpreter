using BuilderInterpreter.ChannelProvidersHelper;
using BuilderInterpreter.Interfaces;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    class MessageReceiver
    {
        private readonly IStateMachine _stateMachineService;
        private readonly BlipChannel _blipChannel;
        private readonly IBlipClient _sender;

        public MessageReceiver(IStateMachine stateMachineService, BlipChannel blipChannel, IBlipClient sender)
        {
            _stateMachineService = stateMachineService;
            _blipChannel = blipChannel;
            _sender = sender;
        }

        public async Task<bool> ReceiveMessage(Message message)
        {
            await _blipChannel.MessageReceiverHelper(message, x => _sender.SendMessageAsync(x, message.From));
            return true;
        }
    }
}
