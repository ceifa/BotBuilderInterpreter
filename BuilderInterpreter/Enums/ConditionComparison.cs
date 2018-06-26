using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderInterpreter.Enums
{
    public enum ConditionComparison
    {
        Equals,
        NotEquals,
        Contains,
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterThanOrEquals,
        LessThanOrEquals,
        Matches,
        ApproximateTo,
    }
}
