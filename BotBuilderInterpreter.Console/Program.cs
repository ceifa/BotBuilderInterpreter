using BotBuilderInterpreter.Console;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BuilderInterpreter.Models;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new BlipClientBuilder().UsingAccessKey("embryoallalfa", "bVFYblJPVW95MnNWY0JhVDdzSzM=").Build();

            var services = new ServiceCollection()
                .AddBuilderInterpreter(new Configuration { AuthorizationKey = "Key ZW1icnlvYWxsYWxmYTptUVhuUk9Vb3kyc1ZjQmFUN3NLMw==" })
                    .AddNoActionHandler<NoAction>()
                    .AddSingleton(client)
                    .AddSingleton<MessageReceiver>()
                    .AddSingleton<Startup>();

            await services.BuildServiceProvider().GetService<Startup>().Start();
        }
    }
}