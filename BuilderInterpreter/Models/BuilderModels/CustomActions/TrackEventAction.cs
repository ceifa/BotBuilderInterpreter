using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class TrackEventAction : TrackEvent, ICustomActionSettingsBase
    {
        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var trackEventService = serviceProvider.GetService<ITrackEventService>();
            var variableService = serviceProvider.GetService<IVariableService>();

            var newTrackEvent = variableService.ReplaceVariablesInObject<TrackEvent>(this, userContext.Variables);

            await trackEventService.RegisterEventTrack(newTrackEvent);
        }
    }
}
