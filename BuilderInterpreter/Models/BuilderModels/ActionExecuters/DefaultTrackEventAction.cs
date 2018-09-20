using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultTrackEventAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IVariableService _variableService;
        private readonly ITrackEventService _trackEventService;

        public DefaultTrackEventAction(IVariableService variableService, ITrackEventService trackEventService)
        {
            _variableService = variableService;
            _trackEventService = trackEventService;
        }

        public CustomActionType ActionType => CustomActionType.TrackEvent;

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<TrackEventAction>();

            FireAndForget.Run(async () =>
            {
                settings = await _variableService.ReplaceVariablesInObjectAsync(settings, userContext);

                await _trackEventService.RegisterEventTrack(settings);
            });
        }
    }
}
