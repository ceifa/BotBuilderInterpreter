using BuilderInterpreter.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ProcessHttp : CustomActionSettingsBase
    {
        [JsonProperty("method")]
        public HttpMethod Method;
        [JsonProperty("headers")]
        public Dictionary<string, string> Headers;
        [JsonProperty("uri")]
        public string Uri;
        [JsonProperty("responseStatusVariable")]
        public string ResponseStatusVariable;
        [JsonProperty("responseBodyVariable")]
        public string ResponseBodyVariable;
    }
}
