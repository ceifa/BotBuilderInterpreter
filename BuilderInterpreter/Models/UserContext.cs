using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Models
{
    public class UserContext
    {
        public string Identity { get; set; }
        public string StateId { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public bool FirstInteraction { get; set; }
    }
}
