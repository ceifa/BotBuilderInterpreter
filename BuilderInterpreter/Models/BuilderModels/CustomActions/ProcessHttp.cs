using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ProcessHttp : ICustomActionSettingsBase
    {
        [JsonProperty("method")]
        public Enums.HttpMethod Method;
        [JsonProperty("headers")]
        public Dictionary<string, string> Headers;
        [JsonProperty("uri")]
        public string Uri;
        [JsonProperty("body")]
        public string Body;
        [JsonProperty("responseStatusVariable")]
        public string ResponseStatusVariable;
        [JsonProperty("responseBodyVariable")]
        public string ResponseBodyVariable;

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
