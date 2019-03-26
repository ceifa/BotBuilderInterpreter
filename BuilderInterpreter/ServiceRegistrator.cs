using BuilderInterpreter.ChannelProvidersHelper;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Models.BuilderModels;
using BuilderInterpreter.Models.BuilderModels.ActionExecuters;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
using BuilderInterpreter.Services;
using BuilderInterpreter.Services.VariableProviders;
using Lime.Protocol.Serialization;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.Linq;
using System.Net.Http;

namespace BuilderInterpreter
{
    public static class ServiceRegistrator
    {
        private const string BlipBaseUri = "https://msging.net";

        public static IServiceCollection AddBuilderInterpreter(this IServiceCollection container, Configuration configuration)
        {
            return container.AddSingleton(BlipProviderFactory())
                .AddSingleton(configuration)
                .AddSingleton<IBlipService, BlipService>()
                .AddSingleton<IBucketBaseService, BucketBaseService>()
                .AddSingleton<IBotFlowService, BotFlowService>()
                .AddSingleton<IBotConfigurationService, BotConfigurationService>()
                .AddSingleton<IUserContextService, UserContextService>()
                .AddSingleton<IStateMachine, StateMachine>()
                .AddSingleton<IUserSemaphoreService, UserSemaphoreService>()
                .AddSingleton<ITrackEventService, TrackEventService>()
                .AddSingleton<IDistributionListService, DistributionListService>()
                .AddSingleton<IVariableService, VariableService>()
                .AddSingleton<IStateMachineService, StateMachineService>()
                .AddSingleton<ICustomActionService, CustomActionService>()
                .AddSingleton<IComparisonService, ComparisonService>()
                .AddSingleton<IVariableProvider, ConfigVariableProvider>()
                .AddSingleton<IVariableProvider, ContactVariableProvider>()
                .AddSingleton<BlipChannel>()
                .AddSingleton<DocumentSerializer>()
                .AddCustomAction<DefaultExecuteScriptAction>()
                .AddCustomAction<DefaultManageListAction>()
                .AddCustomAction<DefaultMergeContactAction>()
                .AddCustomAction<DefaultProcessHttpAction>()
                .AddCustomAction<DefaultRedirectAction>()
                .AddCustomAction<DefaultTrackEventAction>()
                .AddNlpProvider<DefaultNlpProvider>()
                .AddMemoryCache();
        }

        public static IServiceCollection AddNlpProvider<TNlpProvider>(this IServiceCollection container) where TNlpProvider : class, INlpProvider
        {
            var current = container.SingleOrDefault(s => s.ServiceType == typeof(INlpProvider));

            if (current != default)
                container.Remove(current);

            return container.AddSingleton<INlpProvider, TNlpProvider>();
        }

        public static IServiceCollection AddCustomAction<TCustomAction>(this IServiceCollection container) where TCustomAction : class, ICustomAction
        {
            return container.AddSingleton<ICustomAction, TCustomAction>();
        }

        public static IServiceCollection AddNoActionHandler<TNoAction>(this IServiceCollection container) where TNoAction : class, INoAction
        {
            return container.AddSingleton<INoAction, TNoAction>();
        }

        private static IBlipProvider BlipProviderFactory()
        {
            return RestClient.For<IBlipProvider>(new HttpClient(new RetryHandler())
            {
                BaseAddress = new Uri(BlipBaseUri)
            });
        }
    }
}
