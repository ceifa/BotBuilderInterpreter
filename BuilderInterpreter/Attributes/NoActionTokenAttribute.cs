using System;

namespace BuilderInterpreter.Attributes
{
    public class NoActionTokenAttribute : Attribute
    {
        public NoActionTokenAttribute(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}