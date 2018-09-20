using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using Jint;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultExecuteScriptAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IVariableService _variableService;

        public DefaultExecuteScriptAction(IVariableService variableService)
        {
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.ExecuteScript;

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<ExecuteScript>();

            const string defaultFunctionName = "run";
            object[] arguments = null;

            if (settings.InputVariables?.Length > 0)
            {
                arguments = new object[settings.InputVariables.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = await _variableService.GetVariableValueAsync(settings.InputVariables[i], userContext);
                }
            }

            var engine = new Engine(factory => factory.LimitRecursion(5).MaxStatements(50).TimeoutInterval(TimeSpan.FromSeconds(5))).Execute(settings.Source);

            var result = arguments == null ? engine.Invoke(settings.Function ?? defaultFunctionName) : engine.Invoke(settings.Function ?? defaultFunctionName, arguments);

            userContext.SetVariable(settings.OutputVariable, result?.ToObject());
        }
    }
}
