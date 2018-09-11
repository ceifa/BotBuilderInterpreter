using BuilderInterpreter;
using BuilderInterpreter.ConsoleApp;
using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BotBuilderInterpreter.Console
{
    static class ServiceRegistrator
    {
        public static async Task<IServiceProvider> RegisterServices()
        {
            var configuration = new Configuration
            {
                AuthorizationKey = "Key dGVzdGUxNDQ6dkNDb1hSeTNaYUtDVEkxMnNzNWE="
            };
            var client = new BlipClientBuilder().UsingAccessKey("teste144", "dkNDb1hSeTNaYUtDVEkxMnNzNWE=").Build();
            var botCore = new BotCore(configuration);
            var services = await botCore.RegisterDependecies();
            botCore.AddNoActionHandler<NoAction>();
            services.AddSingleton(client);
            services.AddSingleton<MessageReceiver>();
            services.AddSingleton<Startup>();
            return services.BuildServiceProvider();
        }
    }
}
