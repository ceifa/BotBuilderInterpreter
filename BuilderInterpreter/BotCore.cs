using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class BotCore
    {
        private readonly Configuration _configuration;
        private readonly ServiceCollection _serviceDescriptors;

        public BotCore(Configuration configuration, ServiceCollection serviceDescriptors)
        {
            _configuration = configuration;
            _serviceDescriptors = serviceDescriptors;
        }

        public BotCore(Configuration configuration) : this(configuration, new ServiceCollection())
        {
        }

        public Task<ServiceCollection> RegisterDependecies()
        {
            return ServiceContainer.ConfigureServices(_serviceDescriptors, _configuration);
        }

        public BotCore AddNoActionHandler<TNoAction>() where TNoAction : class, INoAction
        {
            ServiceContainer.AddNoActionSingleton<TNoAction>(_serviceDescriptors);
            return this;
        }
    }
}
