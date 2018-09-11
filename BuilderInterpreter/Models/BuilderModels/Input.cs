using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class Input
    {
        [JsonProperty("bypass")]
        public bool Bypass { get; set; }

        [JsonProperty("variable")]
        public string Variable { get; set; }
    }
}
