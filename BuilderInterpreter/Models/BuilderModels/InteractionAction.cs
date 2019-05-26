using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class InteractionAction
    {
        [JsonProperty("input")] public Input Input { get; set; }

        [JsonProperty("action")] public Action Action { get; set; }
    }
}