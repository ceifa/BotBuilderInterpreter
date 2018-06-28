using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IBotFlowService
    {
        Task<BotFlow> GetBotFlow();
    }
}