using System.Collections.Generic;

namespace BuilderInterpreter.Models
{
    public class UserContext
    {
        public string Identity { get; set; }

        public string StateId { get; set; }

        public Dictionary<string, object> Variables { get; set; }

        public UserContact Contact { get; set; }
    }
}
