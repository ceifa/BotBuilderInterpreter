using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class OutputCondition
    {
        [JsonProperty("stateId")]
        public string StateId { get; set; }

        [JsonProperty("conditions")]
        public Condition[] Conditions { get; set; }
    }
}
