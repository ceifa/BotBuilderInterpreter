using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    internal class BotFlowService : IBotFlowService
    {
        private static BotFlow _cachedBotFlow;

        private const string FLOW_BUCKET_KEY = "blip_portal:builder_working_flow";
        private const string CONFIG_BUCKET_KEY = "blip_portal:builder_working_configuration";

        private readonly IBucketBaseService _bucketService;

        public BotFlowService(IBucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<BotFlow> GetBotFlow()
        {
            if (_cachedBotFlow == default)
            {
                var flow = await _bucketService.GetBucketObjectAsync<Dictionary<string, State>>(FLOW_BUCKET_KEY);
                var configuration = await _bucketService.GetBucketObjectAsync<Dictionary<string, object>>(CONFIG_BUCKET_KEY);

                _cachedBotFlow = new BotFlow(flow, configuration);
            }

            return _cachedBotFlow;
        }
    }
}
