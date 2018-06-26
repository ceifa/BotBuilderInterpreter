using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class BotCore
    {
        public Task<ServiceCollection> RegisterDependecies(INoAction noAction, ServiceCollection serviceDescriptors = null)
        {
            return ServiceContainer.ConfigureServices(serviceDescriptors ?? new ServiceCollection(), noAction);
        }

        public BotCore Start(string authKey)
        {
            BlipService.AuthorizationKey = authKey;
            return this;
        }
    }
}
