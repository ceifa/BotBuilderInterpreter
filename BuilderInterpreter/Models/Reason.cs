using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Reason
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
