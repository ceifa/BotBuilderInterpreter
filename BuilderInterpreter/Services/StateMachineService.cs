using BuilderInterpreter.Enums;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BuilderInterpreter
{
    public class StateMachineService : IStateMachineService
    {
        private readonly BotFlow _botFlow;
        private readonly IVariableService _variableService;

        public StateMachineService(BotFlow botFlow, IVariableService variableService)
        {
            _botFlow = botFlow;
            _variableService = variableService;
        }

        public State GetCurrentUserState(UserContext userContext)
        {
            var stateId = userContext.StateId;

            var state = string.IsNullOrEmpty(stateId) ? default : _botFlow.States.SingleOrDefault(x => x.Key == stateId).Value;

            if (state == default)
            {
                state = _botFlow.States.Values.Single(x => x.IsRoot);
            }

            return state;
        }

        public State GetNextUserState(UserContext userContext, State lastState)
        {
            var nextStateId = lastState.DefaultOutput.StateId;

            foreach (var outputCondition in lastState.OutputConditions)
            {
                var matchCondition = outputCondition.Conditions.All(x =>
                {
                    var comparer = CompareCondition(x.Comparison);
                    var input = _variableService.GetVariableValue("input", userContext.Variables).ToString();

                    switch (x.Source)
                    {
                        case ConditionSource.Input:
                            return x.Values.Any(y => comparer(input, y));
                        case ConditionSource.Context:
                            return x.Values.Any(y => comparer(_variableService.ReplaceVariablesInString(x.Variable, userContext.Variables), y));
                        default:
                            throw new NotImplementedException(nameof(x.Source));
                    }
                });

                if (matchCondition)
                {
                    nextStateId = outputCondition.StateId;
                    break;
                }
            }

            return _botFlow.States.Single(x => x.Key == nextStateId).Value;
        }

        private Func<string, string, bool> CompareCondition(ConditionComparison conditionComparison)
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
