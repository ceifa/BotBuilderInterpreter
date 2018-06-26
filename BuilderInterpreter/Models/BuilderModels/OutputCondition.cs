using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class OutputCondition
    {
        [JsonProperty("stateId")]
        public string StateId;
        [JsonProperty("conditions")]
        public Condition[] Conditions;
    }
}
