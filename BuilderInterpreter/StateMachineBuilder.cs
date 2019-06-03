using System;
using System.Linq;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Models.BuilderModels.ActionExecuters;
using BuilderInterpreter.Services;
using BuilderInterpreter.Services.VariableProviders;
using Lime.Protocol.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BuilderInterpreter
{
    public class StateMachineBuilder
    {
        private readonly IServiceCollection _services;

        public StateMachineBuilder()
        {
            _services = new ServiceCollection();
        }

        public IStateMachine Build(Configuration configuration)
        {
            AddCustomAction<DefaultExecuteScriptAction>()
                .AddCustomAction<DefaultManageListAction>()
                .AddCustomAction<DefaultMergeContactAction>()
                .AddCustomAction<DefaultProcessHttpAction>()
                .AddCustomAction<DefaultRedirectAction>()
                .AddCustomAction<DefaultTrackEventAction>();

            if (!IsServiceRegistered<INlpProvider>())
            {
                _services.AddSingleton<INlpProvider, DefaultNlpProvider>();
            }

            return _services.AddSingleton(configuration)
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
                .AddSingleton<DocumentSerializer>()
                .AddMemoryCache()
                .BuildServiceProvider().GetService<IStateMachine>();
        }

        public StateMachineBuilder AddNlpProvider<TNlpProvider>() where TNlpProvider : class, INlpProvider
        {
            if (!IsServiceRegistered<INlpProvider>())
            {
                throw new InvalidOperationException($"A {nameof(INlpProvider)} is already defined.");
            }

            _services.AddSingleton<INlpProvider, TNlpProvider>();
            return this;
        }

        public StateMachineBuilder AddCustomAction<TCustomAction>() where TCustomAction : class, ICustomAction
        {
            _services.AddSingleton<ICustomAction, TCustomAction>();
            return this;
        }

        public StateMachineBuilder UsingMapStorage(IMapStorage mapStorage)
        {
            StorageHelper.Storage = mapStorage;
            return this;
        }

        private bool IsServiceRegistered<TService>()
        {
            return _services.Any(s => s.ServiceType == typeof(TService));
        }
    }
}