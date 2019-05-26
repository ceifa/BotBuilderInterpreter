using System;
using System.Linq;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter
{
    internal class DistributionListService : IDistributionListService
    {
        private const string POSTMASTER_ADDRESS = "postmaster@broadcast.msging.net";
        private const string DISTRIBUTION_LIST_TYPE = "application/vnd.iris.distribution-list+json";
        private const string COMMAND_KEYWORD = "lists";

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
                To = POSTMASTER_ADDRESS,
                Method = CommandMethod.SET,
                Type = "application/vnd.lime.identity",
                Uri = $"/{COMMAND_KEYWORD}/{Uri.EscapeDataString(listIdentity)}/recipients",
                Resource = userIdentity
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<bool> RemoveMemberFromList(string listIdentity, string userIdentity)
        {
            var response = await _blipService.SendCommandAsync(new BlipCommand
            {
                To = POSTMASTER_ADDRESS,
                Method = CommandMethod.DELETE,
                Uri =
                    $"/{COMMAND_KEYWORD}/{Uri.EscapeDataString(listIdentity)}/recipients/{Uri.EscapeDataString(userIdentity)}"
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
                To = POSTMASTER_ADDRESS,
                Method = CommandMethod.SET,
                Type = DISTRIBUTION_LIST_TYPE,
                Uri = $"/{COMMAND_KEYWORD}",
                Resource = new {identity = listIdentity}
            });

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string[]> GetAllListsAsync()
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
            var categories = JArray.FromObject(items).Select(x => x.ToString());

            return categories.ToArray();
        }
    }
}