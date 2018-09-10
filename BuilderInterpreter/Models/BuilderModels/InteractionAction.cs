using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class InteractionAction
    {
        [JsonProperty("input")]
        public Input Input { get; set; }

        [JsonProperty("action")]
        public Action Action { get; set; }
    }
}
