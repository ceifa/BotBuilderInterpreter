using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IBlipService
    {
        Task<BlipCommand> SendCommandAsync(BlipCommand command);
    }
}