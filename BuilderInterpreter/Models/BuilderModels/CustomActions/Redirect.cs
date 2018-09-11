using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Attributes;
using BuilderInterpreter.Models.BuilderModels;

namespace BuilderInterpreter.Models
{
    internal class Redirect : ICustomActionSettingsBase
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("context")]
        public RedirectContext Context { get; set; }

        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var noActionHandlers = serviceProvider.GetService<IEnumerable<INoAction>>();
            var variableService = serviceProvider.GetService<IVariableService>();

            var newRedirect = variableService.ReplaceVariablesInObject(this, userContext.Variables);

            foreach (var noAction in noActionHandlers)
            {
                var method = noAction.GetType().GetMethod(nameof(noAction.ExecuteNoAction));
                var attr = method.GetCustomAttributes(typeof(NoActionTokenAttribute), false);
                
                if (attr?.Length == 1)
                {
                    var token = (NoActionTokenAttribute)attr[0];

                    if (token.Token == newRedirect.Address)
                    {
                        try
                        {
                            await (Task)method.Invoke(noAction, new object[] { newRedirect.Context?.Value, userContext });
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
}
