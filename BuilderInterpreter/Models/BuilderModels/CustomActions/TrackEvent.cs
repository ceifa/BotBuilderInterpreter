using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuilderInterpreter.Models.BuilderModels
{
    class TrackEvent : CustomActionSettingsBase
    {
        [JsonProperty("category")]
        public string Category;
        [JsonProperty("action")]
        public string Action;
        [JsonProperty("extras")]
        public Dictionary<string, string> Extras;
    }
}
