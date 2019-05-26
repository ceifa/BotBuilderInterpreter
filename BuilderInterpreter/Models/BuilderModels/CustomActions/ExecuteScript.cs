using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    public class ExecuteScript
    {
        [JsonProperty("function")] public string Function { get; set; }

        [JsonProperty("inputVariables")] public string[] InputVariables { get; set; }

        [JsonProperty("source")] public string Source { get; set; }

        [JsonProperty("outputVariable")] public string OutputVariable { get; set; }
    }
}