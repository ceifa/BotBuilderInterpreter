using BuilderInterpreter.Interfaces;
using Lime.Protocol;
using Lime.Protocol.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BuilderInterpreter.Services
{
    public class VariableService : IVariableService
    {
        private readonly Regex TextVariablesRegex = new Regex(@"{{([a-zA-Z0-9\.@_\-\[\]]+)}}", RegexOptions.Compiled);

        private DocumentSerializer _serializer;

        public VariableService(DocumentSerializer serializer)
        {
            _serializer = serializer;
        }

        public object GetVariableValue(string name, Dictionary<string, object> variables)
        {
            var variableSplitted = Regex.Split(name, @"\.|(?=\[\d\])", RegexOptions.Compiled);
            if (variableSplitted.Length == 0 || !variables.ContainsKey(variableSplitted[0])) return null;

            var actual = variables[variableSplitted[0]];
            if (variableSplitted.Length == 1) return actual;

            variableSplitted = variableSplitted.Skip(1).ToArray();

            foreach (var variable in variableSplitted)
            {
                var arrayCheck = Regex.Matches(variable, @"\[(\d)\]", RegexOptions.Compiled);

                if (arrayCheck.Count > 0)
                {
                    int index = int.Parse(arrayCheck[0].Groups[1].Value);
                    actual = JArray.FromObject(actual)[index];
                }
                else
                {
                    actual = JObject.FromObject(actual)[variable];
                }

                if (actual == null) return null;
            }

            return actual;
        }

        public void AddOrUpdate(string name, object value, Dictionary<string, object> variables)
        {
            variables[name] = value;
        }

        public string ReplaceVariablesInString(string source, Dictionary<string, object> variables)
        {
            var variableValues = new Dictionary<string, string>();

            foreach (Match match in TextVariablesRegex.Matches(source))
            {
                var variableName = match.Groups[1].Value;
                if (variableValues.ContainsKey(variableName)) continue;
                var variableValue = GetVariableValue(variableName, variables);
                if (variableValue == null) continue;
                variableValues.Add(variableName, variableValue.ToString());
            }

            if (variableValues.Count > 0)
                return TextVariablesRegex.Replace(source, match => variableValues[match.Groups[1].Value]);

            return source;
        }

        public Document ReplaceVariablesInDocument(Document source, Dictionary<string, object> variables)
        {
            var documentSerialized = _serializer.Serialize(source);

            var replaced = ReplaceVariablesInString(documentSerialized, variables);

            if (replaced != documentSerialized)
            {
                return _serializer.Deserialize(replaced, source.GetMediaType());
            }

            return source;
        }

        public T ReplaceVariablesInObject<T>(T source, Dictionary<string, object> variables)
        {
            var sourceSerialized = JsonConvert.SerializeObject(source);
            var replaced = ReplaceVariablesInString(sourceSerialized, variables);
            return JsonConvert.DeserializeObject<T>(replaced);
        }
    }
}
