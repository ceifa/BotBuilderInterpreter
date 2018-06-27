using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuilderInterpreter.Models.BuilderModels
{
    class MergeContact : CustomActionSettingsBase
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("email")]
        public string Email;
        [JsonProperty("phoneNumber")]
        public string PhoneNumber;
        [JsonProperty("city")]
        public string City;
        [JsonProperty("gender")]
        public string Gender;
        [JsonProperty("extras")]
        public Dictionary<string, string> Extras;
    }
}
