using Lime.Protocol;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class Message
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("content")] public Document Content { get; set; }
    }
}