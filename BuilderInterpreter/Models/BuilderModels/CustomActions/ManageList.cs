using System;
using System.Threading.Tasks;
using BuilderInterpreter.Enums;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ManageList : ICustomActionSettingsBase
    {
        [JsonProperty("listName")]
        public string ListName;
        [JsonProperty("action")]
        public ManageListAction Action;

        public async Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var distributionListService = serviceProvider.GetService<DistributionListService>();

            switch (Action)
            {
                case ManageListAction.Add:
                    await distributionListService.AddMemberOrCreateList(ListName, userContext.Identity);
                    break;
                case ManageListAction.Remove:
                    await distributionListService.RemoveMemberFromList(ListName, userContext.Identity);
                    break;
                default:
                    throw new NotImplementedException(nameof(Action));
            }
        }
    }
}