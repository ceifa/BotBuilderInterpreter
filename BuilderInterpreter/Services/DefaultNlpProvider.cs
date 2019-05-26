using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Services
{
    internal class DefaultNlpProvider : INlpProvider
    {
        public Task<NlpResponse> GetNlpResponse(string input, string userIdentity)
        {
            return Task.FromResult<NlpResponse>(null);
        }
    }
}