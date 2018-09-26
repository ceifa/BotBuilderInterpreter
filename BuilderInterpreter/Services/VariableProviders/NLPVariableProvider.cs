using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services.VariableProviders
{
    internal class NLPVariableProvider : IVariableProvider
    {
        public string VariableName => "ai";

        private readonly INlpProvider _nlpProvider;

        public NLPVariableProvider(INlpProvider nlpProvider)
        {
            _nlpProvider = nlpProvider;
        }

        public async Task<object> GetVariableValue(UserContext userContext) => await userContext.NlpResponse;
    }
}
