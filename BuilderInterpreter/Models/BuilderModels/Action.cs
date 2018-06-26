using BuilderInterpreter.Comms;
using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class Action
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("settings")]
        [JsonConverter(typeof(JsonDocumentSerializer))]
        public Message Message;
    }
}
