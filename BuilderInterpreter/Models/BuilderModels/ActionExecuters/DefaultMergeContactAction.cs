using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels.ActionExecuters
{
    internal class DefaultMergeContactAction : ICustomAction, IDefaultCustomAction
    {
        private readonly IVariableService _variableService;

        public DefaultMergeContactAction(IVariableService variableService)
        {
            _variableService = variableService;
        }

        public CustomActionType ActionType => CustomActionType.MergeContact;

        public async Task ExecuteActionAsync(UserContext userContext, ICustomActionPayload payload)
        {
            var settings = payload as MergeContact;

            settings = _variableService.ReplaceVariablesInObject(settings, userContext.Variables);
            userContext.Contact = CustomActionHelper.MergeObjects(userContext.Contact, settings);
        }
    }
}
