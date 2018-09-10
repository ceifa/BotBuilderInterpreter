using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuilderInterpreter.Models
{
    public class TrackEvent
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("extras")]
        public Dictionary<string, string> Extras { get; set; }
    }
}
