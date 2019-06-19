using System.Threading.Tasks;
using BuilderInterpreter.Enums;
using BuilderInterpreter.Models;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomAction
    {
        CustomActionType ActionType { get; }

        Task ExecuteActionAsync(UserContext userContext, JObject payload);
    }
}