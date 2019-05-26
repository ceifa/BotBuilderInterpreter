using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface INoAction
    {
        Task ExecuteNoAction(string extras, UserContext userContext);
    }
}