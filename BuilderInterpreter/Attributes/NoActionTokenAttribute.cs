using System;

namespace BuilderInterpreter.Attributes
{
    public class NoActionTokenAttribute : Attribute
    {
        public string Token { get; }

        public NoActionTokenAttribute(string token)
        {
            Token = token;
        }
    }
}
