using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Reason
    {
        [JsonProperty("code")]
        public int Code;
        [JsonProperty("description")]
        public string Description;
    }
}
