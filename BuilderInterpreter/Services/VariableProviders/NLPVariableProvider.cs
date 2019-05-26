using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Services.VariableProviders
{
    internal class NlpVariableProvider : IVariableProvider
    {
        public string VariableName => "ai";

        public async Task<object> GetVariableValue(UserContext userContext)
        {
            return await userContext.NlpResponse;
        }
    }
}