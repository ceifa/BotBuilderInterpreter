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

        private readonly IBucketBaseService _bucketService;

        public BotFlowService(IBucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<BotFlow> GetBotFlow()
        {
            if (_cachedBotFlow == default)
            {
                _cachedBotFlow = new BotFlow(await _bucketService.GetBucketObjectAsync<Dictionary<string, State>>(FLOW_BUCKET_KEY));
            }

            return _cachedBotFlow;
        }
    }
}
