using Lime.Protocol;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    public class Message
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("content")]
        public Document Content;
    }
}
