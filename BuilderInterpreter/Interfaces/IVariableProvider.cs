using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface IVariableProvider
    {
        string VariableName { get; }

        Task<object> GetVariableValue(UserContext userContext);
    }
}