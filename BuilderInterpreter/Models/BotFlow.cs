using System.Collections.Generic;

namespace BuilderInterpreter.Models
{
    internal class BotFlow
    {
        public BotFlow(Dictionary<string, State> states)
        {
            States = states;
        }

        public Dictionary<string, State> States { get; set; }
    }
}
