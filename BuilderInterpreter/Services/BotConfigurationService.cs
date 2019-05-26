using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;

namespace BuilderInterpreter.Services
{
    internal class BotConfigurationService : IBotConfigurationService
    {
        private const string CONFIG_BUCKET_KEY = "blip_portal:builder_working_configuration";
        private static Dictionary<string, string> _cachedBotConfiguration;

        private readonly IBucketBaseService _bucketService;

        public BotConfigurationService(IBucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<Dictionary<string, string>> GetBotConfigurationAsync()
        {
            return _cachedBotConfiguration ?? (_cachedBotConfiguration =
                       await _bucketService.GetBucketObjectAsync<Dictionary<string, string>>(CONFIG_BUCKET_KEY));
        }
    }
}