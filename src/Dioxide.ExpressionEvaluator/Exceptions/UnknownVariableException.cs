using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class UnknownVariableException : Exception
{
    public UnknownVariableException(string message)
        : base(message) { }
}
