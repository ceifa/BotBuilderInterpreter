using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    class TrackEventService : ITrackEventService
    {
        private readonly IBlipService _blipService;

        public TrackEventService(IBlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> RegisterEventTrack(TrackEvent trackEvent)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = "postmaster@analytics.msging.net",
                Method = CommandMethod.SET,
                Type = "application/vnd.iris.eventTrack+json",
                Uri = "/event-track",
                Resource = trackEvent
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string[]> GetAllEventTrackCategories()
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = "postmaster@analytics.msging.net",
                Method = CommandMethod.GET,
                Uri = "/event-track"
            });

            if (response.Status != CommandStatus.SUCCESS)
                return default;

            var items = JObject.FromObject(response.Resource)["items"];
            var categories = JArray.FromObject(items).Select(x => x["category"].ToString());

            return categories.ToArray();
        }
    }
}
