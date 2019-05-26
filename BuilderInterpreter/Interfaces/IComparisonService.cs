using System;

namespace BuilderInterpreter.Interfaces
{
    internal interface IComparisonService
    {
        ComparisonType GetComparisonType(ConditionComparison conditionComparison);

        Func<string, string, bool> GetBinaryConditionComparator(ConditionComparison conditionComparison);

        Func<string, bool> GetUnaryConditionComparator(ConditionComparison conditionComparison);
    }
}