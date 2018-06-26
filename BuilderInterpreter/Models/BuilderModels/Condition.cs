using BuilderInterpreter.Enums;
using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class Condition
    {
        [JsonProperty("source")]
        public ConditionSource Source;

        [JsonProperty("comparison")]
        public ConditionComparison Comparison;

        [JsonProperty("values")]
        public string[] Values;

        [JsonProperty("variable")]
        public string Variable;
    }
}
