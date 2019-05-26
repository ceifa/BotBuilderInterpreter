using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Redirect
    {
        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("context")] public RedirectContext Context { get; set; }
    }
}