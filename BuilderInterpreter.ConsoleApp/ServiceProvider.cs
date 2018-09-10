using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    static class ServiceProvider
    {
        public static async Task<Microsoft.Extensions.DependencyInjection.ServiceProvider> RegisterServices(IBlipClient blipClient, BotCore botCore)
        {
            var configuration = new Configuration
            {
                AuthorizationKey = "Key dGVzdGUxNDQ6dkNDb1hSeTNaYUtDVEkxMnNzNWE="
            };

            services.AddSingleton<MessageReceiver>();
            services.AddSingleton(blipClient);
            return services.BuildServiceProvider();
        }
    }
}
