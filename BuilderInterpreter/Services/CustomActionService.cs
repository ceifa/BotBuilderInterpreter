using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System;

namespace BuilderInterpreter.Services
{
    class CustomActionService : ICustomActionService
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomActionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ExecuteCustomActions(CustomAction[] customActions, UserContext userContext)
        {
            foreach (var customAction in customActions)
            {
                customAction.Settings.Execute(userContext, _serviceProvider);
            }
        }
    }
}
