using System.Threading.Tasks;
using BuilderInterpreter.ChannelProvidersHelper;
using Lime.Protocol;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    internal class MessageReceiver
    {
        private readonly BlipChannel _blipChannel;
        private readonly IBlipClient _sender;

        public MessageReceiver(BlipChannel blipChannel, IBlipClient sender)
        {
            _blipChannel = blipChannel;
            _sender = sender;
        }

        public async Task<bool> ReceiveMessageAsync(Message message)
        {
            if (message.Metadata.ContainsKey("#resentCount"))
                return false;

            await _blipChannel.MessageReceiverHelper(message, x => _sender.SendMessageAsync(x, message.From));
            return true;
        }
    }
}