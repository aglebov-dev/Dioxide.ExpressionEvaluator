using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class EvaluationInternalException : Exception
{
    public EvaluationInternalException(string message)
        : base(message) { }
}
