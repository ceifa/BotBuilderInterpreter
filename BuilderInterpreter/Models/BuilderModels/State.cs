using BuilderInterpreter.Comms;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class State
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("$title")]
        public string Title;
        [JsonProperty("$contentActions")]
        public InteractionAction[] InteractionActions;
        [JsonProperty("$enteringCustomActions")]
        [JsonConverter(typeof(JsonCustomActionArraySerializer))]
        public CustomAction[] EnteringCustomActions;
        [JsonProperty("$leavingCustomActions")]
        [JsonConverter(typeof(JsonCustomActionArraySerializer))]
        public CustomAction[] LeavingCustomActions;
        [JsonProperty("$conditionOutputs")]
        public OutputCondition[] OutputConditions;
        [JsonProperty("$defaultOutput")]
        public OutputCondition DefaultOutput;
        [JsonProperty("root")]
        public bool IsRoot;
    }
}
