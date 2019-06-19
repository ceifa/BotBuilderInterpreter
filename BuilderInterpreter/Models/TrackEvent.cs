using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    internal class TrackEvent
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("extras")]
        public Dictionary<string, string> Extras { get; set; }
    }
}