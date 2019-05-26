using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class ProcessHttp
    {
        [JsonProperty("method")] public HttpMethod Method { get; set; }

        [JsonProperty("headers")] public Dictionary<string, string> Headers { get; set; }

        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("body")] public string Body { get; set; }

        [JsonProperty("responseStatusVariable")]
        public string ResponseStatusVariable { get; set; }

        [JsonProperty("responseBodyVariable")] public string ResponseBodyVariable { get; set; }
    }
}