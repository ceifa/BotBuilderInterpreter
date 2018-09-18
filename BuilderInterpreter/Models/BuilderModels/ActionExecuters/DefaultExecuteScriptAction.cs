using BuilderInterpreter.Interfaces;
using Jint;
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

        public async Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload)
        {
            var settings = payload as ExecuteScript;

            const string defaultFunctionName = "run";
            object[] arguments = null;

            if (settings.InputVariables?.Length > 0)
            {
                arguments = new object[settings.InputVariables.Length];

                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = _variableService.GetVariableValue(settings.InputVariables[i], userContext.Variables);
                }
            }

            var engine = new Engine(factory => factory.LimitRecursion(5).MaxStatements(50).TimeoutInterval(TimeSpan.FromSeconds(5))).Execute(settings.Source);

            var result = arguments == null ? engine.Invoke(settings.Function ?? defaultFunctionName) : engine.Invoke(settings.Function ?? defaultFunctionName, arguments);

            _variableService.AddOrUpdate(settings.OutputVariable, result?.ToObject(), userContext.Variables);
        }
    }
}
