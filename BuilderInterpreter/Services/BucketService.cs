using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class BucketService : IBucketService
    {
        private const string COMMAND_KEYWORD = "buckets";

        private readonly IBlipService _blipService;

        public BucketService(IBlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> DeleteBucketAsync(string key)
        {
            var uri = $"/{COMMAND_KEYWORD}/{Uri.EscapeDataString(key)}";
            
            var command = new BlipCommand
            {
                Method = CommandMethod.DELETE,
                Uri = uri,
            };

            var response = await _blipService.SendCommandAsync(command);

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string> GetBucketAsync(string key)
        {
            var uri = $"/{COMMAND_KEYWORD}/{Uri.EscapeDataString(key)}";

            var command = new BlipCommand
            {
                Method = CommandMethod.GET,
                Uri = uri
            };

            var response = await _blipService.SendCommandAsync(command);

            if (response.Status != CommandStatus.SUCCESS)
                return default;

            return JsonConvert.SerializeObject(response.Resource);
        }

        public async Task<bool> SetBucketAsync(string key, object document, TimeSpan expiration = default)
        {
            var uri = $"/{COMMAND_KEYWORD}/{Uri.EscapeDataString(key)}";

            if (expiration != default)
                uri += $"?expiration={expiration.TotalMilliseconds}";

            var command = new BlipCommand
            {
                Method = CommandMethod.SET,
                Uri = uri,
                Type = "application/json",
                Resource = document
            };

            var response = await _blipService.SendCommandAsync(command);

            return response.Status == CommandStatus.SUCCESS;
        }
    }
}
