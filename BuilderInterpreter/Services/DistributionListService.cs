using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    internal class DistributionListService : IDistributionListService
    {
        private const string To = "postmaster@broadcast.msging.net";
        private const string Type = "application/vnd.iris.distribution-list+json";
        private const string UriPrefix = "/lists";

        private readonly IBlipService _blipService;

        public DistributionListService(IBlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> AddMemberOrCreateList(string listIdentity, string userIdentity)
        {
            var lists = await GetAllListsAsync();
            var hasList = lists.Any(x => x == listIdentity);

            if (!hasList)
                await CreateListAsync(listIdentity);

            return await AddMemberToList(listIdentity, userIdentity);
        }

        public async Task<bool> AddMemberToList(string listIdentity, string userIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = CommandMethod.SET,
                Type = "application/vnd.lime.identity",
                Uri = $"{UriPrefix}/{listIdentity}/recipients",
                Resource = userIdentity
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<bool> RemoveMemberFromList(string listIdentity, string userIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = CommandMethod.DELETE,
                Uri = $"{UriPrefix}/{listIdentity}/recipients/{userIdentity}",
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<bool> SendMessageToList(string listIdentity, Document message)
        {
            var content = JObject.FromObject(message)["content"].ToObject<Document>();

            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = listIdentity,
                Type = message.GetMediaType().ToString(),
                Content = content
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<bool> CreateListAsync(string listIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = CommandMethod.SET,
                Type = Type,
                Uri = UriPrefix,
                Resource = new { identity = listIdentity }
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string[]> GetAllListsAsync()
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = "postmaster@broadcast.msging.net",
                Method = CommandMethod.GET,
                Uri = UriPrefix
            });

            if (response.Status != CommandStatus.SUCCESS)
                return default;

            var items = JObject.FromObject(response.Resource)["items"];
            var categories = JArray.FromObject(items).Select(x => x.ToString());

            return categories.ToArray();
        }
    }
}
