using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class BotFlow
    {
        public BotFlow(Dictionary<string, State> states, Dictionary<string, object> globalVariables)
        {
            States = states;
            GlobalVariables = globalVariables;
        }

        public Dictionary<string, State> States;
        public Dictionary<string, object> GlobalVariables;
    }
}
