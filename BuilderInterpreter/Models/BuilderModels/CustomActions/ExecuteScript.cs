using Microsoft.Extensions.DependencyInjection;
using BuilderInterpreter.Interfaces;
using Jint;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ExecuteScript : ICustomActionSettingsBase
    {
        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("inputVariables")]
        public string[] InputVariables { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("outputVariable")]
        public string OutputVariable { get; set; }

        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var variableService = serviceProvider.GetService<IVariableService>();

            const string defaultFunctionName = "run";
            object[] arguments = null;

            if (InputVariables?.Length > 0)
            {
                arguments = new object[InputVariables.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = variableService.GetVariableValue(InputVariables[i], userContext.Variables);
                }
            }

            var engine = new Engine(factory => factory.LimitRecursion(5).MaxStatements(50).TimeoutInterval(TimeSpan.FromSeconds(5))).Execute(Source);
            
            var result = arguments == null ? engine.Invoke(Function ?? defaultFunctionName) : engine.Invoke(Function ?? defaultFunctionName, arguments);

            variableService.AddOrUpdate(OutputVariable, result?.ToObject(), userContext.Variables);

            return Task.CompletedTask;
        }
    }
}
