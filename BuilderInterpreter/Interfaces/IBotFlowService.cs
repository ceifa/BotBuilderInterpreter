using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    internal interface IBotFlowService
    {
        Task<BotFlow> GetBotFlow();
    }
}