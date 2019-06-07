using System;
using System.Collections.Generic;

namespace BuilderInterpreter.Protocol
{
    public class Envelope
    {
        public string Id { get; set; }

        public Message Message { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}
