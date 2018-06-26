using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class Reason
    {
        [JsonProperty("code")]
        public int Code;
        [JsonProperty("description")]
        public string Description;
    }
}
