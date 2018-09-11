using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBlipService
    {
        Task<BlipCommand> SendCommandAsync(BlipCommand command);
    }
}