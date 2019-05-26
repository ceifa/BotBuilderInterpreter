using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Services.VariableProviders
{
    internal class ContactVariableProvider : IVariableProvider
    {
        public string VariableName => "contact";

        public Task<object> GetVariableValue(UserContext userContext)
        {
            return Task.FromResult<object>(userContext.Contact);
        }
    }
}