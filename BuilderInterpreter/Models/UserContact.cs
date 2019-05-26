using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class UserContact
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phoneNumber")] public string PhoneNumber { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("gender")] public string Gender { get; set; }

        [JsonProperty("extras")] public Dictionary<string, string> Extras { get; set; }
    }
}