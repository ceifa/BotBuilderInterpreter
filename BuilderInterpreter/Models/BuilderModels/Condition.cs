using BuilderInterpreter.Enums;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class Condition
    {
        [JsonProperty("source")] public ConditionSource Source { get; set; }

        [JsonProperty("comparison")] public ConditionComparison Comparison { get; set; }

        [JsonProperty("values")] public string[] Values { get; set; }

        [JsonProperty("variable")] public string Variable { get; set; }

        [JsonProperty("entity")] public string Entity { get; set; }
    }
}