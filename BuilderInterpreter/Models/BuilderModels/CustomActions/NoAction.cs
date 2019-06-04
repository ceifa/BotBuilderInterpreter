using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class NoAction
    {
        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("context")] public RedirectContext Context { get; set; }
    }
}