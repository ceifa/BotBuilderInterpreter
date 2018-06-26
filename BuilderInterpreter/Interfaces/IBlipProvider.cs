using BuilderInterpreter.Models;
using RestEase;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IBlipProvider
    {
        [Header("Authorization")]
        string AuthorizationKey { get; set; }

        [Header("Content-Type", "application/json")]
        [Post("commands")]
        Task<BlipCommand> SendCommandAsync([Body] BlipCommand command);
    }
}
