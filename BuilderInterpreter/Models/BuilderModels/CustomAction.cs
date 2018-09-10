using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class CustomAction
    {
        [JsonProperty("type")]
        public CustomActionType Type { get; set; }

        [JsonProperty("$title")]
        public string Title { get; set; }

        [JsonProperty("settings")]
        public ICustomActionSettingsBase Settings { get; set; }
    }
}