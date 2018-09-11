using BuilderInterpreter.Comms;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class State
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("$title")]
        public string Title { get; set; }

        [JsonProperty("$contentActions")]
        public InteractionAction[] InteractionActions { get; set; }

        [JsonProperty("$enteringCustomActions")]
        [JsonConverter(typeof(JsonCustomActionArraySerializer))]
        public CustomAction[] EnteringCustomActions { get; set; }

        [JsonProperty("$leavingCustomActions")]
        [JsonConverter(typeof(JsonCustomActionArraySerializer))]
        public CustomAction[] LeavingCustomActions { get; set; }

        [JsonProperty("$conditionOutputs")]
        public OutputCondition[] OutputConditions { get; set; }

        [JsonProperty("$defaultOutput")]
        public OutputCondition DefaultOutput { get; set; }

        [JsonProperty("root")]
        public bool IsRoot { get; set; }
    }
}
