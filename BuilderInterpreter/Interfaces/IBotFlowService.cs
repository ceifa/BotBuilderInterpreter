using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBotFlowService
    {
        Task<BotFlow> GetBotFlow();
    }
}