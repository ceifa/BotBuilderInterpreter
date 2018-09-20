using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuilderInterpreter.Models
{
    public class Intent
    {
        [JsonProperty("id")]
        public string IntentId { get; set; }

        [JsonProperty("name")]
        public string IntentName { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("extras")]
        public Dictionary<string, object> Extras { get; set; }
    }
}
