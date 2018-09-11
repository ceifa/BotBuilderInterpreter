using BuilderInterpreter.ConsoleApp;
using Lime.Protocol.Listeners;
using System.Threading;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BotBuilderInterpreter.Console
{
    class Startup
    {
        private readonly IBlipClient _blipClient;
        private readonly MessageReceiver _messageReceiver;

        public Startup(IBlipClient blipClient, MessageReceiver messageReceiver)
        {
            _blipClient = blipClient;
            _messageReceiver = messageReceiver;
        }

        public async Task Start()
        {
            await _blipClient.StartAsync(new ChannelListener(_messageReceiver.ReceiveMessage, _ => Task.FromResult(true), _ => Task.FromResult(true)), CancellationToken.None);
            System.Console.WriteLine("Bot Started");
            System.Console.ReadKey();
        }
    }
}
