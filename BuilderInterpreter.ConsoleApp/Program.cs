using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using Lime.Protocol.Listeners;
using System;
using System.Threading;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    class Program
    {
        static IBlipClient _blipClient;
        static MessageReceiver _messageReceiver;

        static void Main(string[] args)
        {
            _blipClient = new BlipClientBuilder().UsingAccessKey("teste144", "dkNDb1hSeTNaYUtDVEkxMnNzNWE=").Build();
            Starter().GetAwaiter().GetResult();
        }

        static async Task Starter()
        {
            var botCore = new BotCore();
            var provider = await ServiceProvider.RegisterServices(_blipClient, botCore);
            _messageReceiver = provider.GetService<MessageReceiver>();
            await _blipClient.StartAsync(new ChannelListener(_messageReceiver.ReceiveMessage, x => Task.FromResult(true), x => Task.FromResult(true)), CancellationToken.None);
            Console.WriteLine("Bot Started");
            Console.ReadKey();
        }
    }

    class NoAction : INoAction
    {
        public Task ExecuteNoAction(string userIdentity, string value, UserContext userContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
