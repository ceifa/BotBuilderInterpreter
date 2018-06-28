using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class DistributionListService : IDistributionListService
    {
        private const string To = "postmaster@broadcast.msging.net";
        private const string Type = "application/vnd.iris.distribution-list+json";
        private const string UriPrefix = "/lists";
        private const string UriPostFix = "@broadcast.msging.net";

        private readonly IBlipService _blipService;

        public DistributionListService(IBlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> AddMemberOrCreateList(string listIdentity, string userIdentity)
        {
            var lists = await GetAllListsAsync();
            var hasList = lists.Any(x => x == Uri.EscapeDataString(listIdentity));

            if (!hasList)
                await CreateListAsync(listIdentity);

            return await AddMemberToList(listIdentity, userIdentity);
        }

        public async Task<bool> AddMemberToList(string listIdentity, string userIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = Enums.CommandMethod.SET,
                Type = "application/vnd.lime.identity",
                Uri = $"{UriPrefix}/{Uri.EscapeDataString(listIdentity)}{UriPostFix}/recipients",
                Resource = userIdentity
            });

            return response.Status == Enums.CommandStatus.SUCCESS;
        }

        public async Task<bool> RemoveMemberFromList(string listIdentity, string userIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = Enums.CommandMethod.DELETE,
                Uri = $"{UriPrefix}/{Uri.EscapeDataString(listIdentity)}{UriPostFix}/recipients/{userIdentity}",
            });

            return response.Status == Enums.CommandStatus.SUCCESS;
        }

        public async Task<bool> SendMessageToList(string listIdentity, Document message)
        {
            var content = JObject.FromObject(message)["content"].ToObject<Document>();

            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = $"{Uri.EscapeDataString(listIdentity)}{UriPostFix}",
                Type = message.GetMediaType().ToString(),
                Content = content
            });

            return response.Status == Enums.CommandStatus.SUCCESS;
        }

        public async Task<bool> CreateListAsync(string listIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = To,
                Method = Enums.CommandMethod.SET,
                Type = Type,
                Uri = UriPrefix,
                Resource = new { identity = $"{Uri.EscapeDataString(listIdentity)}{UriPostFix}" }
            });

            return response.Status == Enums.CommandStatus.SUCCESS;
        }

        public async Task<string[]> GetAllListsAsync()
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = "postmaster@broadcast.msging.net",
                Method = Enums.CommandMethod.GET,
                Uri = UriPrefix
            });

            if (response.Status != Enums.CommandStatus.SUCCESS)
                return default;

            var items = JObject.FromObject(response.Resource)["items"];
            var categories = JArray.FromObject(items).Select(x => x.ToString());

            return categories.ToArray();
        }
    }
}
