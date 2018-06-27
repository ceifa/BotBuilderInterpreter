using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ExecuteScript : CustomActionSettingsBase
    {
        [JsonProperty("function")]
        public string Function;
        [JsonProperty("inputVariables")]
        public string[] InputVariables;
        [JsonProperty("source")]
        public string Source;
        [JsonProperty("outputVariable")]
        public string OutputVariable;
    }
}
