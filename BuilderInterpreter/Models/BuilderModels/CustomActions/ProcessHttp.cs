using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class ProcessHttp : ICustomActionSettingsBase
    {
        [JsonProperty("method")]
        public HttpMethod Method { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("responseStatusVariable")]
        public string ResponseStatusVariable { get; set; }

        [JsonProperty("responseBodyVariable")]
        public string ResponseBodyVariable { get; set; }

        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var variableService = serviceProvider.GetService<IVariableService>();

            var newProccessHttp = variableService.ReplaceVariablesInObject(this, userContext.Variables);
            var responseMessage = await newProccessHttp.ExecuteHttpRequest();

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var responseStatusCode = responseMessage.StatusCode;

            variableService.AddOrUpdate(ResponseStatusVariable, (int)responseStatusCode, userContext.Variables);
            variableService.AddOrUpdate(ResponseBodyVariable, responseBody, userContext.Variables);
        }
    }
}
