using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    class CustomActionService : ICustomActionService
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomActionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteCustomActions(CustomAction[] customActions, UserContext userContext)
        {
            foreach (var customAction in customActions)
            {
                await customAction.Settings.Execute(userContext, _serviceProvider);
            }
        }
    }
}
