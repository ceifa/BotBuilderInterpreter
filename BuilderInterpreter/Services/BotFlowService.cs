using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal class BotFlowService : IBotFlowService
    {
        private const string FLOW_BUCKET_KEY = "blip_portal:builder_working_flow";
        private static BotFlow _cachedBotFlow;

        private readonly IBucketBaseService _bucketService;

        public BotFlowService(IBucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<BotFlow> GetBotFlow()
        {
            return _cachedBotFlow ?? (_cachedBotFlow =
                       new BotFlow(
                           await _bucketService.GetBucketObjectAsync<Dictionary<string, State>>(FLOW_BUCKET_KEY)));
        }
    }
}