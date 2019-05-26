using System.Threading.Tasks;
using BotBuilderInterpreter.Console;
using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new BlipClientBuilder().UsingAccessKey("embryoallalfa", "bVFYblJPVW95MnNWY0JhVDdzSzM=")
                .Build();

            var services = new ServiceCollection()
                .AddBuilderInterpreter(new Configuration
                    {AuthorizationKey = "Key ZW1icnlvYWxsYWxmYTptUVhuUk9Vb3kyc1ZjQmFUN3NLMw=="})
                .AddNoActionHandler<NoAction>()
                .AddSingleton(client)
                .AddSingleton<MessageReceiver>()
                .AddSingleton<Startup>();

            await services.BuildServiceProvider().GetService<Startup>().Start();
        }
    }
}