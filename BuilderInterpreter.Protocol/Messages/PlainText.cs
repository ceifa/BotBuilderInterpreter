using System;
namespace BuilderInterpreter.Protocol.Messages
{
    public class PlainText : Message
    {
        public override string Type => "text/plain";

        public string Value { get; set; }

        public PlainText(string value)
        {
            Value = value;
        }

        public static implicit operator PlainText(string value) => new PlainText(value);

        public override string ToString() => Value;
    }
}
