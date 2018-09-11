using BuilderInterpreter.ChannelProvidersHelper;
using Lime.Protocol;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    class MessageReceiver
    {
        private readonly BlipChannel _blipChannel;
        private readonly IBlipClient _sender;

        public MessageReceiver(BlipChannel blipChannel, IBlipClient sender)
        {
            _blipChannel = blipChannel;
            _sender = sender;
        }

        public async Task<bool> ReceiveMessage(Message message)
        {
            if (message.Metadata.ContainsKey("#resentCount"))
                return false;

            await _blipChannel.MessageReceiverHelper(message, x => _sender.SendMessageAsync(x, message.From));
            return true;
        }
    }
}