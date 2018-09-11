using BotBuilderInterpreter.Console;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BuilderInterpreter.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = await ServiceRegistrator.RegisterServices();
            await services.GetService<Startup>().Start();
        }
    }
}