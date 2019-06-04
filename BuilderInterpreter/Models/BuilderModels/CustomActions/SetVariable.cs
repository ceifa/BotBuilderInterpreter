using System;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels.CustomActions
{
    public class SetVariable
    {
        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("value")] public object Value { get; set; }
    }
}
