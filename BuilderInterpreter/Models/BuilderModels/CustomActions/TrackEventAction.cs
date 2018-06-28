using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class TrackEventAction : TrackEvent, ICustomActionSettingsBase
    {
        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var trackEventService = serviceProvider.GetService<TrackEventService>();
            await trackEventService.RegisterEventTrack(this);
        }
    }
}
