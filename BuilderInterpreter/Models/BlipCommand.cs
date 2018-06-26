using BuilderInterpreter.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class BlipCommand
    {
        public BlipCommand(string id)
        {
            Id = id;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("resource", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Resource { get; set; }

        [JsonProperty("method")]
        public CommandMethod Method { get; set; }

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CommandStatus Status { get; set; }

        [JsonProperty("reason", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Reason Reason { get; set; }
    }
}
