using Newtonsoft.Json;
using System.Collections.Generic;

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
