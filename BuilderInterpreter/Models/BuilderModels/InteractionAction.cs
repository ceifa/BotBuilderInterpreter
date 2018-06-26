using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class InteractionAction
    {
        [JsonProperty("input")]
        public Input Input;
        [JsonProperty("action")]
        public Action Action;
    }
}
