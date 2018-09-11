using System.Collections.Generic;

namespace BuilderInterpreter.Models
{
    internal class BotFlow
    {
        public BotFlow(Dictionary<string, State> states, Dictionary<string, object> globalVariables)
        {
            States = states;
            GlobalVariables = globalVariables;
        }

        public Dictionary<string, State> States { get; set; }
        public Dictionary<string, object> GlobalVariables { get; set; }
    }
}
