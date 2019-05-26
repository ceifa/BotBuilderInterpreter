using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using Lime.Protocol;
using Lime.Protocol.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuilderInterpreter.Services
{
    internal class VariableService : IVariableService
    {
        private readonly DocumentSerializer _serializer;
        private readonly IEnumerable<IVariableProvider> _variableProviders;

        private readonly Regex TextVariablesRegex =
            new Regex(@"{{([a-zA-Z_$][a-zA-Z0-9_$[\.\]]*)}}", RegexOptions.Compiled);

        public VariableService(DocumentSerializer serializer, IEnumerable<IVariableProvider> variableProviders)
        {
            _serializer = serializer;
            _variableProviders = variableProviders;
        }

        public async Task<object> GetVariableValueAsync(string name, UserContext userContext)
        {
            var variableSplit = Regex.Split(name, @"\.|(?=\[\d\])", RegexOptions.Compiled);

            if (variableSplit.Length == 0)
                return null;

            var provider = _variableProviders.SingleOrDefault(p => p.VariableName == variableSplit[0]);

            object value;

            if (provider != default)
                value = await provider.GetVariableValue(userContext);
            else
                value = userContext.GetVariable(variableSplit[0]);

            value = GetVariableValueInObject(variableSplit, value);

            return value != null && value.ToString() == value.GetType().ToString()
                ? JsonConvert.SerializeObject(value)
                : value?.ToString();
        }

        public async Task<string> ReplaceVariablesInStringAsync(string source, UserContext userContext)
        {
            var variableValues = new Dictionary<string, string>();

            foreach (Match match in TextVariablesRegex.Matches(source))
            {
                var variableName = match.Groups[1].Value;

                if (variableValues.ContainsKey(variableName))
                    continue;

                var variableValue = await GetVariableValueAsync(variableName, userContext);

                if (variableValue == null)
                    continue;

                variableValues.Add(variableName, variableValue.ToString());
            }

            if (variableValues.Count > 0)
                return TextVariablesRegex.Replace(source,
                    match => variableValues.ContainsKey(match.Groups[1].Value)
                        ? variableValues[match.Groups[1].Value]
                        : null);

            return source;
        }

        public async Task<Document> ReplaceVariablesInDocumentAsync(Document source, UserContext userContext)
        {
            var documentSerialized = _serializer.Serialize(source);

            var replaced = await ReplaceVariablesInStringAsync(documentSerialized, userContext);

            if (replaced != documentSerialized) return _serializer.Deserialize(replaced, source.GetMediaType());

            return source;
        }

        public async Task<T> ReplaceVariablesInObjectAsync<T>(T source, UserContext userContext)
        {
            var sourceSerialized = JsonConvert.SerializeObject(source);
            var replaced = await ReplaceVariablesInStringAsync(sourceSerialized, userContext);
            return JsonConvert.DeserializeObject<T>(replaced);
        }

        public static object GetVariableValueInObject(string[] variableSplitted, object source)
        {
            for (var i = 1; i < variableSplitted.Length; i++)
            {
                var arrayCheck = Regex.Matches(variableSplitted[i], @"\[(\d)\]", RegexOptions.Compiled);

                if (arrayCheck.Count > 0)
                {
                    var index = int.Parse(arrayCheck[0].Groups[1].Value);
                    source = JArray.FromObject(source)?[index];
                }
                else
                {
                    source = JObject.FromObject(source)?[variableSplitted[i]];
                }

                if (source == null)
                    return null;
            }

            return source;
        }

        public static object GetVariableValueInObject(string name, object source)
        {
            var variableSplitted = Regex.Split(name, @"\.|(?=\[\d\])", RegexOptions.Compiled);

            if (variableSplitted.Length == 0)
                return null;

            return GetVariableValueInObject(variableSplitted, source);
        }
    }
}