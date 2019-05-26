using System;
using System.Linq;
using System.Threading.Tasks;
using BuilderInterpreter.Extensions;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal class StateMachineService : IStateMachineService
    {
        private readonly IBotFlowService _botFlowService;
        private readonly IComparisonService _comparisonService;
        private readonly IVariableService _variableService;

        public StateMachineService(IBotFlowService botFlowService, IVariableService variableService,
            IComparisonService comparisonService)
        {
            _variableService = variableService;
            _comparisonService = comparisonService;
            _botFlowService = botFlowService;
        }

        public State GetCurrentUserState(UserContext userContext, BotFlow botFlow)
        {
            var stateId = userContext.StateId;

            var state = string.IsNullOrEmpty(stateId) ? default : botFlow.States[stateId];

            if (state == default) state = botFlow.States.Values.Single(x => x.IsRoot);

            return state;
        }

        public async Task<State> GetNextUserStateAsync(UserContext userContext, State lastState, BotFlow botFlow)
        {
            var nextStateId = lastState.DefaultOutput.StateId;

            foreach (var outputCondition in lastState.OutputConditions)
            {
                var matchCondition = await outputCondition.Conditions.AllAsync(async o =>
                {
                    string toCompare;

                    switch (o.Source)
                    {
                        case ConditionSource.Input:
                            toCompare = (await _variableService.GetVariableValueAsync("input", userContext)).ToString();
                            break;

                        case ConditionSource.Context:
                            toCompare = await _variableService.ReplaceVariablesInStringAsync(o.Variable, userContext);
                            break;

                        case ConditionSource.Intent:
                            toCompare = (await userContext.NlpResponse)?.Intent?.IntentName;
                            break;

                        case ConditionSource.Entity:
                            toCompare = (await userContext.NlpResponse)?.Entities?[o.Entity];
                            break;

                        default:
                            throw new NotImplementedException(nameof(o.Source));
                    }

                    var comparisonType = _comparisonService.GetComparisonType(o.Comparison);

                    switch (comparisonType)
                    {
                        case ComparisonType.Unary:
                            var unaryComparer = _comparisonService.GetUnaryConditionComparator(o.Comparison);
                            return unaryComparer(toCompare);

                        case ComparisonType.Binary:
                            var binaryComparer = _comparisonService.GetBinaryConditionComparator(o.Comparison);
                            return o.Values.Any(v => binaryComparer(toCompare, v));

                        default:
                            throw new NotImplementedException(nameof(comparisonType));
                    }
                });

                if (matchCondition)
                {
                    nextStateId = outputCondition.StateId;
                    break;
                }
            }

            return botFlow.States[nextStateId] ?? botFlow.States.Single(x => x.Value.IsRoot).Value;
        }
    }
}