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
            var client = new BlipClientBuilder().UsingAccessKey("teste144", "dkNDb1hSeTNaYUtDVEkxMnNzNWE=").Build();

            var services = new ServiceCollection()
                .AddBuilderInterpreter(new Configuration { AuthorizationKey = "Key dGVzdGUxNDQ6dkNDb1hSeTNaYUtDVEkxMnNzNWE=" })
                    .AddNoActionHandler<NoAction>()
                    .AddSingleton(client)
                    .AddSingleton<MessageReceiver>()
                    .AddSingleton<Startup>();

            await services.BuildServiceProvider().GetService<Startup>().Start();
        }
    }
}