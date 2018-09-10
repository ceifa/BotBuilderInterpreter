using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using Lime.Protocol;
using Lime.Messaging.Contents;

namespace BuilderInterpreter.Models
{
    class Redirect : ICustomActionSettingsBase
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("context")]
        public Document Context { get; set; }

        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var noActionHandlers = serviceProvider.GetService<IEnumerable<INoAction>>();
            var variableService = serviceProvider.GetService<IVariableService>();

            var newRedirect = variableService.ReplaceVariablesInObject(this, userContext.Variables);
            var extras = newRedirect.Context as PlainText;

            noActionHandlers.ForEach(async n => await n.ExecuteNoAction(newRedirect.Address, extras?.Text, userContext));

            return Task.CompletedTask;
        }
    }
}
