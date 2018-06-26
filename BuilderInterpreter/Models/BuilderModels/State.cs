using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class State
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("$title")]
        public string Title;
        [JsonProperty("$contentActions")]
        public InteractionAction[] InteractionActions;
        [JsonProperty("$conditionOutputs")]
        public OutputCondition[] OutputConditions;
        [JsonProperty("$defaultOutput")]
        public OutputCondition DefaultOutput;
    }
}
