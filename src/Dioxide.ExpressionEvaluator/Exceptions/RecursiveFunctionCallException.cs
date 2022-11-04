using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class RecursiveFunctionCallException : Exception
{
    public RecursiveFunctionCallException(string message)
        : base(message) { }
}
