using BuilderInterpreter.Enums;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Models;
using Lime.Protocol;
using Lime.Protocol.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BuilderInterpreter.Helper
{
    internal static class StateMachineHelper
    {
        private static readonly Regex TextVariablesRegex = new Regex(@"{{([a-zA-Z0-9\.@_\-\[\]]+)}}", RegexOptions.Compiled);
        private static DocumentSerializer _serializer = new DocumentSerializer();

        public static string GetVariableValue(string variableName, Dictionary<string, object> variables)
        {
            var variableSplitted = Regex.Split(variableName, @"\.|(?=\[\d\])", RegexOptions.Compiled);
            if (variableSplitted.Length == 0 || !variables.ContainsKey(variableSplitted[0])) return null;

            var actual = variables[variableSplitted[0]];
            if (variableSplitted.Length == 1) return actual.ToString();

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

            return actual.ToString();
        }

        public static Document GetDocumentWithVariablesReplaced(Document document, MediaType mediaType, Dictionary<string, object> variables)
        {
            var documentSerialized = _serializer.Serialize(document);

            var variableValues = new Dictionary<string, string>();
            foreach (Match match in TextVariablesRegex.Matches(documentSerialized))
            {
                var variableName = match.Groups[1].Value;
                if (variableValues.ContainsKey(variableName)) continue;
                var variableValue = GetVariableValue(variableName, variables);
                if (variableValue == null) continue;
                variableValues.Add(variableName, variableValue);
            }

            if (variableValues.Count > 0)
            {
                var replaced = TextVariablesRegex.Replace(documentSerialized, match => variableValues[match.Groups[1].Value]);
                return _serializer.Deserialize(replaced, mediaType);
            }

            return document;
        }

        internal static string GetNewStateId(string input, Dictionary<string, object> variables, OutputCondition[] outputConditions, OutputCondition defaultOutput)
        {
            foreach (var outputCondition in outputConditions)
            {
                var matchCondition = outputCondition.Conditions.All(x =>
                {
                    var comparer = CompareCondition(x.Comparison);

                    switch (x.Source)
                    {
                        case ConditionSource.Input:
                            return x.Values.Any(y => comparer(input, y));
                        case ConditionSource.Context:
                            return x.Values.Any(y => comparer(StateMachineHelper.GetVariableValue(x.Variable, variables), y));
                        default:
                            throw new NotImplementedException(nameof(x.Source));
                    }
                });

                if (matchCondition) return outputCondition.StateId;
            }

            return defaultOutput.StateId;
        }

        private static Func<string, string, bool> CompareCondition(ConditionComparison conditionComparison)
        {
            switch (conditionComparison)
            {
                case ConditionComparison.Equals:
                    return (v1, v2) => string.Compare(v1, v2, CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;

                case ConditionComparison.NotEquals:
                    return (v1, v2) => string.Compare(v1, v2, CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) != 0;

                case ConditionComparison.Contains:
                    return (v1, v2) => v1 != null && v2 != null && v1.IndexOf(v2, StringComparison.OrdinalIgnoreCase) >= 0;

                case ConditionComparison.StartsWith:
                    return (v1, v2) => v1 != null && v2 != null && v1.StartsWith(v2, StringComparison.OrdinalIgnoreCase);

                case ConditionComparison.EndsWith:
                    return (v1, v2) => v1 != null && v2 != null && v1.EndsWith(v2, StringComparison.OrdinalIgnoreCase);

                case ConditionComparison.Matches:
                    return (v1, v2) => v1 != null && v2 != null && Regex.IsMatch(v1, v2);

                case ConditionComparison.ApproximateTo:
                    return (v1, v2) => v1 != null && v2 != null && v1.ToLowerInvariant().CalculateLevenshteinDistance(v2.ToLowerInvariant()) <= Math.Ceiling(v1.Length * 0.25);

                case ConditionComparison.GreaterThan:
                    return (v1, v2) => decimal.TryParse(v1, out var n1) && decimal.TryParse(v2, out var n2) && n1 > n2;

                case ConditionComparison.LessThan:
                    return (v1, v2) => decimal.TryParse(v1, out var n1) && decimal.TryParse(v2, out var n2) && n1 < n2;

                case ConditionComparison.GreaterThanOrEquals:
                    return (v1, v2) => decimal.TryParse(v1, out var n1) && decimal.TryParse(v2, out var n2) && n1 >= n2;

                case ConditionComparison.LessThanOrEquals:
                    return (v1, v2) => decimal.TryParse(v1, out var n1) && decimal.TryParse(v2, out var n2) && n1 <= n2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditionComparison));
            }

        }
    }
}
