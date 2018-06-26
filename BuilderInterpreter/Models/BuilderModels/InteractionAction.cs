using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class InteractionAction
    {
        [JsonProperty("input")]
        public Input Input;
        [JsonProperty("action")]
        public Action Action;
    }
}
