using System;
using Lime.Protocol;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    public class BlipCommand
    {
        public BlipCommand()
        {
            Id = Guid.NewGuid().ToString();
        }

        public BlipCommand(string id)
        {
            Id = id;
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("to", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string To { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("resource", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Resource { get; set; }

        [JsonProperty("content", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Document Content { get; set; }

        [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CommandMethod? Method { get; set; }

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CommandStatus? Status { get; set; }

        [JsonProperty("reason", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Reason Reason { get; set; }
    }
}