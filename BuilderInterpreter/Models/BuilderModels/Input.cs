using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Input
    {
        [JsonProperty("bypass")]
        public bool Bypass;

        [JsonProperty("variable")]
        public string Variable;
    }
}
