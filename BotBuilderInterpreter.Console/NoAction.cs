using System.Threading.Tasks;
using BuilderInterpreter.Attributes;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BotBuilderInterpreter.Console
{
    class NoAction : INoAction
    {
        [NoActionToken("testee")]
        public Task ExecuteNoAction(string extras, UserContext userContext)
        {
            return Task.CompletedTask;
        }
    }
}
