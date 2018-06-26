using BuilderInterpreter.Comms;
using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Action
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("settings")]
        [JsonConverter(typeof(JsonDocumentSerializer))]
        public Message Message;
    }
}
