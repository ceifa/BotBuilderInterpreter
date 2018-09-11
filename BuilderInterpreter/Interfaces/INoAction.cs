using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface INoAction
    {
        Task ExecuteNoAction(string extras, UserContext userContext);
    }
}
