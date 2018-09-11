using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class RedirectContext
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
