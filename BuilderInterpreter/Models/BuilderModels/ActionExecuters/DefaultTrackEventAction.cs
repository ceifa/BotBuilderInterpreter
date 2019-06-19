using System.Threading.Tasks;
using BuilderInterpreter.Enums;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultTrackEventAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IVariableService _variableService;

        public DefaultTrackEventAction(IVariableService variableService)
        {
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.TrackEvent;

        public Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<TrackEventAction>();

            return Task.CompletedTask;
        }
    }
}