using System;
using BuilderInterpreter.Protocol.Messages;

namespace BuilderInterpreter.Protocol
{
    public abstract class Message
    {
        public abstract string Type { get; }

        public static implicit operator Message(string value) => new PlainText(value);
    }
}
