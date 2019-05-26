using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Intent
    {
        [JsonProperty("name")] public string IntentName { get; set; }

        [JsonProperty("score")] public int Score { get; set; }

        [JsonProperty("extras")] public Dictionary<string, object> Extras { get; set; }
    }
}