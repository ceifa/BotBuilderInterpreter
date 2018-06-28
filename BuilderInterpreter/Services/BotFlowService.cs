using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public class BotFlowService : IBotFlowService
    {
        private const string FlowBucketKey = "blip_portal:builder_working_flow";
        private const string ConfigurationBucketKey = "blip_portal:builder_working_configuration";

        private readonly IBucketBaseService _bucketService;

        public BotFlowService(IBucketBaseService bucketService)
        {
            _bucketService = bucketService;
        }

        public async Task<BotFlow> GetBotFlow()
        {
            var flow = await _bucketService.GetBucketObjectAsync<Dictionary<string, State>>(FlowBucketKey);
            var configuration = await _bucketService.GetBucketObjectAsync<Dictionary<string, object>>(ConfigurationBucketKey);
            return new BotFlow(flow, configuration);
        }
    }
}
