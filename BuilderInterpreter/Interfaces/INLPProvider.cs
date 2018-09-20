using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface INlpProvider
    {
        Task<NlpResponse> GetNlpResponse(string input, string userIdentity);
    }
}
