using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models.BuilderModels
{
    class MergeContact : UserContact, ICustomActionSettingsBase
    {
        public Task Execute(UserContext userContext, IServiceProvider serviceProvider)
        {
            var newContact = CustomActionHelper.MergeObjects(userContext.Contact, this);
            userContext.Contact = newContact;

            return Task.CompletedTask;
        }
    }
}
