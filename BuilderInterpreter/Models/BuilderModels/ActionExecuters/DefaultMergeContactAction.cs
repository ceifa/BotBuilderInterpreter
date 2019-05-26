using System.Threading.Tasks;
using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using Newtonsoft.Json.Linq;

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

        public async Task ExecuteActionAsync(UserContext userContext, JObject payload)
        {
            var settings = payload.ToObject<MergeContact>();

            settings = await _variableService.ReplaceVariablesInObjectAsync(settings, userContext);
            userContext.Contact = CustomActionHelper.MergeObjects(userContext.Contact, settings);
        }
    }
}