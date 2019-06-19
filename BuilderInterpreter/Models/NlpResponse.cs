using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class NlpResponse
    {
        [JsonProperty("intent")]
        public Intent Intent { get; set; }

        [JsonProperty("entities")]
        public Dictionary<string, string> Entities { get; set; }
    }
}