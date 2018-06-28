using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace BuilderInterpreter.ConsoleApp
{
    static class ServiceProvider
    {
        public static async Task<Microsoft.Extensions.DependencyInjection.ServiceProvider> RegisterServices(IBlipClient blipClient)
        {
            var services = await new BotCore().Start("Key dGVzdGUxNDQ6dkNDb1hSeTNaYUtDVEkxMnNzNWE=").RegisterDependecies(new NoAction());
            services.AddSingleton<MessageReceiver>();
            services.AddSingleton(blipClient);
            return services.BuildServiceProvider();
        }
    }
}
