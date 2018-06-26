using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class Input
    {
        [JsonProperty("bypass")]
        public bool Bypass;

        [JsonProperty("variable")]
        public string Variable;
    }
}
