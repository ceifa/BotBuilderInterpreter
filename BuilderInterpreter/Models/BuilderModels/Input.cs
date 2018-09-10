﻿using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class Input
    {
        [JsonProperty("bypass")]
        public bool Bypass { get; set; }

        [JsonProperty("variable")]
        public string Variable { get; set; }
    }
}
