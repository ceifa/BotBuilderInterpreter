using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    public interface IBlipService
    {
        Task<BlipCommand> SendCommandAsync(BlipCommand command);
    }
}