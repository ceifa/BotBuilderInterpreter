using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class OutputCondition
    {
        [JsonProperty("stateId")]
        public string StateId;
        [JsonProperty("conditions")]
        public Condition[] Conditions;
    }
}
