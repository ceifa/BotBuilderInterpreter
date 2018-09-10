using System;
using System.Threading.Tasks;
using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class ManageList : ICustomActionSettingsBase
    {
        [JsonProperty("listName")]
        public string ListName { get; set; }

        [JsonProperty("action")]
        public ManageListAction Action { get; set; }

        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var distributionListService = serviceProvider.GetService<IDistributionListService>();
            var variableService = serviceProvider.GetService<IVariableService>();

            var listNameReplaced = variableService.ReplaceVariablesInString(ListName, userContext.Variables);

            switch (Action)
            {
                case ManageListAction.Add:
                    await distributionListService.AddMemberOrCreateList(listNameReplaced, userContext.Identity);
                    break;
                case ManageListAction.Remove:
                    await distributionListService.RemoveMemberFromList(listNameReplaced, userContext.Identity);
                    break;
                default:
                    throw new NotImplementedException(nameof(Action));
            }
        }
    }
}