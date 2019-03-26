using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services.VariableProviders
{
    internal class NlpVariableProvider : IVariableProvider
    {
        public string VariableName => "ai";

        public async Task<object> GetVariableValue(UserContext userContext) => await userContext.NlpResponse;
    }
}
