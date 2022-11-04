using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class UnknownFunctionException : Exception
{
    public UnknownFunctionException(string message)
        : base(message) { }
}
