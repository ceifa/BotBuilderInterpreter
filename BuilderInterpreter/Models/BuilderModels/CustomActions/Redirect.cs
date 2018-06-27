using System;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using Lime.Protocol;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models
{
    class Redirect : ICustomActionSettingsBase
    {
        [JsonProperty("address")]
        public string Address;
        [JsonProperty("context")]
        public Document Context;

        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            throw new NotImplementedException("Redirect is not supported yet.");
        }
    }
}
