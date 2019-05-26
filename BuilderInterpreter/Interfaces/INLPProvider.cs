using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface INlpProvider
    {
        Task<NlpResponse> GetNlpResponse(string input, string userIdentity);
    }
}