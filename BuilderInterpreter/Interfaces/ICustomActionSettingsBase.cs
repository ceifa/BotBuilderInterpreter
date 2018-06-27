using BuilderInterpreter.Models;
using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomActionSettingsBase
    {
        Task Execute(UserContext userContext, IServiceProvider serviceProvider);
    }
}
