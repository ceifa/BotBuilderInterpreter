using BuilderInterpreter.Models;
using Lime.Protocol;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IStateMachine
    {
        Task<Document[]> HandleUserInput(string userIdentity, string input, UserContext userContext);
    }
}