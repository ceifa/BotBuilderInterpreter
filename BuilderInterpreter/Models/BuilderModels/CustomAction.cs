using BuilderInterpreter.Comms;
using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class CustomAction
    {
        [JsonProperty("type")]
        public CustomActionType Type;
        [JsonProperty("$title")]
        public string Title;
        [JsonProperty("settings")]
        public ICustomActionSettingsBase Settings;
    }
}