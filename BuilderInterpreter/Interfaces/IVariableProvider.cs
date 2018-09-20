using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IVariableProvider
    {
        string VariableName { get; }

        Task<object> GetVariableValue(UserContext userContext);
    }
}
