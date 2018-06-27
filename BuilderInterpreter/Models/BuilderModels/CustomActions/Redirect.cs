using Lime.Protocol;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    class Redirect : CustomActionSettingsBase
    {
        [JsonProperty("address")]
        public string Address;
        [JsonProperty("context")]
        public Document Context;
    }
}
