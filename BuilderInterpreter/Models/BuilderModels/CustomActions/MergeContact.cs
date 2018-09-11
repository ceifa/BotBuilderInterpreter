using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class MergeContact : UserContact, ICustomActionSettingsBase
    {
        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var variableService = serviceProvider.GetService<IVariableService>();

            var newContact = variableService.ReplaceVariablesInObject<UserContact>(this, userContext.Variables);
            newContact = CustomActionHelper.MergeObjects(userContext.Contact, newContact);
            userContext.Contact = newContact;

            return Task.CompletedTask;
        }
    }
}
