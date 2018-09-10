using BuilderInterpreter.ChannelProvidersHelper;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Services;
using Lime.Protocol.Serialization;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public static class ServiceContainer
    {
        internal static async Task<ServiceCollection> ConfigureServices(ServiceCollection container, Configuration configuration)
        {
            container.AddSingleton(BlipProviderFactory());
            container.AddSingleton(configuration);
            container.AddSingleton<IBlipService, BlipService>();
            container.AddSingleton<IBucketBaseService, BucketBaseService>();
            container.AddSingleton<IBotFlowService, BotFlowService>();
            container.AddSingleton<IUserContextService, UserContextService>();
            container.AddSingleton<IStateMachine, StateMachine>();
            container.AddSingleton<IUserSemaphoreService, UserSemaphoreService>();
            container.AddSingleton<ITrackEventService, TrackEventService>();
            container.AddSingleton<IDistributionListService, DistributionListService>();
            container.AddSingleton<IVariableService, VariableService>();
            container.AddSingleton<IStateMachineService, StateMachineService>();
            container.AddSingleton<ICustomActionService, CustomActionService>();
            container.AddSingleton<BlipChannel>();
            container.AddSingleton<DocumentSerializer>();
            container.AddMemoryCache();
            container.AddSingleton(await BotFlowFactory(container));

            return container;
        }

        internal static void AddNoActionSingleton<TNoAction>(ServiceCollection container) where TNoAction : class, INoAction
        {
            container.AddSingleton<INoAction, TNoAction>();
        }

        private static async Task<BotFlow> BotFlowFactory(ServiceCollection container)
        {
            var provider = container.BuildServiceProvider();

            var botFlowService = provider.GetService<IBotFlowService>();
            var botFlow = await botFlowService.GetBotFlow();

            return botFlow ?? throw new NullReferenceException(nameof(botFlow));
        }

        private static IBlipProvider BlipProviderFactory()
        {
            return RestClient.For<IBlipProvider>(new HttpClient(new RetryHandler())
            {
                BaseAddress = new Uri("https://msging.net")
            });
        }
    }
}
