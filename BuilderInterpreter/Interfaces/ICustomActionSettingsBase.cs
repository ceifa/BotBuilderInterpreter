using BuilderInterpreter.Models;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface ICustomActionSettingsBase
    {
        Task Execute(UserContext userContext, IServiceProvider serviceProvider);
    }
}
