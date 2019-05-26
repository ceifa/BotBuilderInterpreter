using System.Threading.Tasks;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultTrackEventAction : ICustomAction, IDefaultCustomAction
    {
        private readonly ITrackEventService _trackEventService;
        private readonly IVariableService _variableService;

        public DefaultTrackEventAction(IVariableService variableService, ITrackEventService trackEventService)
        {
            _variableService = variableService;
            _trackEventService = trackEventService;
        }

        public CustomActionType ActionType => CustomActionType.TrackEvent;

        public Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<TrackEventAction>();

            FireAndForget.Run(async () =>
            {
                settings = await _variableService.ReplaceVariablesInObjectAsync(settings, userContext);

                await _trackEventService.RegisterEventTrack(settings);
            });

            return Task.CompletedTask;
        }
    }
}