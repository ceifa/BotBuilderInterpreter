using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Services;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class TrackEventAction : TrackEvent, ICustomActionSettingsBase
    {
        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var trackEventService = (TrackEventService)serviceProvider.GetService(typeof(TrackEventService));
            await trackEventService.RegisterEventTrack(this);
        }
    }
}
