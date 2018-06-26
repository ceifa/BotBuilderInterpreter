using BuilderInterpreter.Enums;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Condition
    {
        [JsonProperty("source")]
        public ConditionSource Source;

        [JsonProperty("comparison")]
        public ConditionComparison Comparison;

        [JsonProperty("values")]
        public string[] Values;

        [JsonProperty("variable")]
        public string Variable;
    }
}
