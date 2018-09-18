using BuilderInterpreter.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface ICustomAction
    {
        CustomActionType ActionType { get; }

        Task ExecuteActionAsync(UserContext userContext, JObject payload);
    }
}
