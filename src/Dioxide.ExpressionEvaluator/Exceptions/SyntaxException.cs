using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class SyntaxException : Exception
{
    public SyntaxException(string message)
        : base(message) { }
}
