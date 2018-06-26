using BuilderInterpreter.ChannelProvidersHelper;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public static class ServiceContainer
    {
        public static async Task<ServiceCollection> ConfigureServices(ServiceCollection container, INoAction noAction)
        {
            container.AddSingleton(BlipProviderFactory());
            container.AddSingleton<BlipService>();
            container.AddSingleton<BucketBaseService>();
            container.AddSingleton<BotFlowService>();
            container.AddSingleton<IUserContextService, UserContextService>();
            container.AddSingleton<StateMachineService>();
            container.AddSingleton<BlipChannel>();
            container.AddSingleton<IMemoryCache, MemoryCache>();
            container.AddSingleton<UserSemaphoreService>();
            container.AddSingleton(noAction);
            container.AddSingleton(await BotFlowFactory(container));

            return container;
        }

        private static Task<BotFlow> BotFlowFactory(ServiceCollection container)
        {
            var botFlowService = container.BuildServiceProvider().GetService<BotFlowService>();
            var botFlow = botFlowService.GetBotFlow();
            if (botFlow == null) throw new NullReferenceException(nameof(botFlow));
            return botFlow;
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
