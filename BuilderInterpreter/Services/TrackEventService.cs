using System.Linq;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter
{
    internal class TrackEventService : ITrackEventService
    {
        private const string POSTMASTER_ADDRESS = "postmaster@analytics.msging.net";
        private const string COMMAND_KEYWORD = "event-track";

        private readonly IBlipService _blipService;

        public TrackEventService(IBlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> RegisterEventTrack(TrackEvent trackEvent)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = POSTMASTER_ADDRESS,
                Method = CommandMethod.SET,
                Type = "application/vnd.iris.eventTrack+json",
                Uri = $"/{COMMAND_KEYWORD}",
                Resource = trackEvent
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string[]> GetAllEventTrackCategories()
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = POSTMASTER_ADDRESS,
                Method = CommandMethod.GET,
                Uri = $"/{COMMAND_KEYWORD}"
            });

            if (response.Status != CommandStatus.SUCCESS)
                return default;

            var items = JObject.FromObject(response.Resource)["items"];
            var categories = JArray.FromObject(items).Select(x => x["category"].ToString());

            return categories.ToArray();
        }
    }
}