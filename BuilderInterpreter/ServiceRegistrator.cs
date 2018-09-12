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
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddBuilderInterpreter(this IServiceCollection container, Configuration configuration)
        {
            return container.AddSingleton(BlipProviderFactory())
                .AddSingleton(configuration)
                .AddSingleton<IBlipService, BlipService>()
                .AddSingleton<IBucketBaseService, BucketBaseService>()
                .AddSingleton<IBotFlowService, BotFlowService>()
                .AddSingleton<IUserContextService, UserContextService>()
                .AddSingleton<IStateMachine, StateMachine>()
                .AddSingleton<IUserSemaphoreService, UserSemaphoreService>()
                .AddSingleton<ITrackEventService, TrackEventService>()
                .AddSingleton<IDistributionListService, DistributionListService>()
                .AddSingleton<IVariableService, VariableService>()
                .AddSingleton<IStateMachineService, StateMachineService>()
                .AddSingleton<ICustomActionService, CustomActionService>()
                .AddSingleton<IComparisonService, ComparisonService>()
                .AddSingleton<BlipChannel>()
                .AddSingleton<DocumentSerializer>()
                .AddMemoryCache()
                .AddSingleton(BotFlowFactory(container).GetAwaiter().GetResult());
        }

        public static IServiceCollection AddNoActionHandler<TNoAction>(this IServiceCollection container) where TNoAction : class, INoAction
        {
            return container.AddSingleton<INoAction, TNoAction>();
        }

        private static async Task<BotFlow> BotFlowFactory(IServiceCollection container)
        {
            var provider = container.BuildServiceProvider();

            var botFlowService = provider.GetService<IBotFlowService>();
            var botFlow = await botFlowService.GetBotFlow().ConfigureAwait(false);

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
