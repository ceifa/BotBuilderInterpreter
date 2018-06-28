using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class BotCore
    {
        public Task<ServiceCollection> RegisterDependecies(Configuration configuration, INoAction noAction, ServiceCollection serviceDescriptors = null)
        {
            return ServiceContainer.ConfigureServices(serviceDescriptors ?? new ServiceCollection(), configuration, noAction);
        }
    }
}
