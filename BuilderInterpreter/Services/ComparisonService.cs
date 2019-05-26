using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;

namespace BuilderInterpreter.Services
{
    internal class ComparisonService : IComparisonService
    {
        public ComparisonType GetComparisonType(ConditionComparison conditionComparison)
        {
            switch (conditionComparison)
            {
                case ConditionComparison.Exists:
                case ConditionComparison.NotExists:
                    return ComparisonType.Unary;

                case ConditionComparison.Equals:
                case ConditionComparison.NotEquals:
                case ConditionComparison.Contains:
                case ConditionComparison.StartsWith:
                case ConditionComparison.EndsWith:
                case ConditionComparison.GreaterThan:
                case ConditionComparison.LessThan:
                case ConditionComparison.GreaterThanOrEquals:
                case ConditionComparison.LessThanOrEquals:
                case ConditionComparison.Matches:
                case ConditionComparison.ApproximateTo:
                    return ComparisonType.Binary;

                default:
                    throw new ArgumentOutOfRangeException(nameof(conditionComparison));
            }
        }

        public Func<string, string, bool> GetBinaryConditionComparator(ConditionComparison conditionComparison)
        {
            switch (conditionComparison)
            {
                case ConditionComparison.Equals:
                    return (v1, v2) => string.Compare(v1, v2, CultureInfo.InvariantCulture,
                                           CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;

                case ConditionComparison.NotEquals:
                    return (v1, v2) => string.Compare(v1, v2, CultureInfo.InvariantCulture,
                                           CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) != 0;

                case ConditionComparison.Contains:
                    return (v1, v2) =>
                        v1 != null && v2 != null && v1.IndexOf(v2, StringComparison.OrdinalIgnoreCase) >= 0;

                case ConditionComparison.StartsWith:
                    return (v1, v2) =>
                        v1 != null && v2 != null && v1.StartsWith(v2, StringComparison.OrdinalIgnoreCase);

                case ConditionComparison.EndsWith:
                    return (v1, v2) => v1 != null && v2 != null && v1.EndsWith(v2, StringComparison.OrdinalIgnoreCase);

                case ConditionComparison.Matches:
                    return (v1, v2) => v1 != null && v2 != null && Regex.IsMatch(v1, v2);

                case ConditionComparison.ApproximateTo:
                    return (v1, v2) =>
                        v1 != null && v2 != null &&
                        v1.ToLowerInvariant().CalculateLevenshteinDistance(v2.ToLowerInvariant()) <=
                        Math.Ceiling(v1.Length * 0.25);

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

        public Func<string, bool> GetUnaryConditionComparator(ConditionComparison conditionComparison)
        {
            switch (conditionComparison)
            {
                case ConditionComparison.Exists:
                    return v => !string.IsNullOrEmpty(v);

                case ConditionComparison.NotExists:
                    return v => string.IsNullOrEmpty(v);

                default:
                    throw new ArgumentOutOfRangeException(nameof(conditionComparison));
            }
        }
    }
}