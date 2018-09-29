using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    internal class DefaultNlpProvider : INlpProvider
    {
        public Task<NlpResponse> GetNlpResponse(string input, string userIdentity) => Task.FromResult<NlpResponse>(null);
    }
}
