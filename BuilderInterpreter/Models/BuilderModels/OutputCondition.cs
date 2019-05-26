using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class OutputCondition
    {
        [JsonProperty("stateId")] public string StateId { get; set; }

        [JsonProperty("conditions")] public Condition[] Conditions { get; set; }
    }
}