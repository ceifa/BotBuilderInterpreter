using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models
{
    public class UserContact
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
