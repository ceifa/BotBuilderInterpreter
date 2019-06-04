using System;
using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    public class DefaultSetVariable : ICustomAction, IDefaultCustomAction
    {
        public CustomActionType ActionType => CustomActionType.SetVariable;

        public Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<SetVariable>();

            userContext.SetVariable(settings.Key, settings.Value);
            return Task.CompletedTask;
        }
    }
}
