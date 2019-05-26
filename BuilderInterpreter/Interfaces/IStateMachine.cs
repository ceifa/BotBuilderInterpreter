using System.Threading.Tasks;
using BuilderInterpreter.Models;
using Lime.Protocol;

namespace BuilderInterpreter.Interfaces
{
    public interface IStateMachine
    {
        Task<Document[]> HandleUserInput(string userIdentity, string input, UserContext userContext);
    }
}