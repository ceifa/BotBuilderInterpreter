using BuilderInterpreter.Helper;
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
        public string Function;
        [JsonProperty("inputVariables")]
        public string[] InputVariables;
        [JsonProperty("source")]
        public string Source;
        [JsonProperty("outputVariable")]
        public string OutputVariable;

        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            const string defaultFunctionName = "run";
            object[] arguments = null;

            if (InputVariables != null && InputVariables.Length > 0)
            {
                arguments = new object[InputVariables.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = StateMachineHelper.GetVariableValue(InputVariables[i], userContext.Variables);
                }
            }

            var engine = new Engine(factory => factory.LimitRecursion(5).MaxStatements(50).TimeoutInterval(TimeSpan.FromSeconds(5))).Execute(Source);
            
            var result = arguments == null ? engine.Invoke(Function ?? defaultFunctionName) : engine.Invoke(Function ?? defaultFunctionName, arguments);

            userContext.Variables[OutputVariable] = result?.ToObject();

            return Task.CompletedTask;
        }
    }
}
