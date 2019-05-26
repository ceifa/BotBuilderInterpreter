using System.Threading.Tasks;
using BuilderInterpreter.Models;
using RestEase;

namespace BuilderInterpreter.Interfaces
{
    public interface IBlipProvider
    {
        [Header("Authorization")] string AuthorizationKey { get; set; }

        [Header("Content-Type", "application/json")]
        [Post("commands")]
        Task<BlipCommand> SendCommandAsync([Body] BlipCommand command);
    }
}