using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models.BuilderModels.CustomActions;
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

        public async Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload)
        {
            var settings = payload as TrackEventAction;

            FireAndForget.Run(async () =>
            {
                settings = _variableService.ReplaceVariablesInObject(settings, userContext.Variables);

                await _trackEventService.RegisterEventTrack(settings);
            });
        }
    }
}
