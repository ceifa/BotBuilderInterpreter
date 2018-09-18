using Newtonsoft.Json;
using BuilderInterpreter.Models.BuilderModels;
using BuilderInterpreter.Interfaces;

namespace BuilderInterpreter.Models
{
    public class Redirect : ICustomActionPayload
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("context")]
        public RedirectContext Context { get; set; }
    }
}
