using Lime.Protocol;
using System.Collections.Generic;

namespace BuilderInterpreter.Interfaces
{
    public interface IVariableService
    {
        object GetVariableValue(string name, Dictionary<string, object> variables);

        void AddOrUpdate(string name, object value, Dictionary<string, object> variables);

        string ReplaceVariablesInString(string source, Dictionary<string, object> variables);

        Document ReplaceVariablesInDocument(Document source, Dictionary<string, object> variables);

        T ReplaceVariablesInObject<T>(T source, Dictionary<string, object> variables);
    }
}
