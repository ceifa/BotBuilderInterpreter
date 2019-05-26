using BuilderInterpreter.Models;

namespace BuilderInterpreter.Extensions
{
    public static class UserContextExtensions
    {
        public static object GetVariable(this UserContext userContext, string key)
        {
            return userContext.Variables.ContainsKey(key) ? userContext.Variables[key] : null;
        }

        public static void SetVariable(this UserContext userContext, string key, object value)
        {
            userContext.Variables[key] = value;
        }
    }
}