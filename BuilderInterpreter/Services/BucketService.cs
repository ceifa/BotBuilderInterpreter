using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    public class BucketService : IBucketService
    {
        private const string CommandKeyword = "buckets";

        private readonly BlipService _blipService;

        public BucketService(BlipService blipService)
        {
            _blipService = blipService;
        }

        public async Task<bool> DeleteBucketAsync(string key)
        {
            var uri = $"/{CommandKeyword}/{key}";
            
            var command = new BlipCommand(Guid.NewGuid().ToString())
            {
                Method = CommandMethod.DELETE,
                Uri = uri,
            };

            var response = await _blipService.SendCommandAsync(command);

            return response.Status == CommandStatus.SUCCESS;
        }

        public async Task<string> GetBucketAsync(string key)
        {
            var uri = $"/{CommandKeyword}/{key}";

            var command = new BlipCommand(Guid.NewGuid().ToString())
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
            var uri = $"/{CommandKeyword}/{key}";

            if (expiration != default)
                uri += $"?expiration={expiration.TotalMilliseconds}";

            var command = new BlipCommand(Guid.NewGuid().ToString())
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
