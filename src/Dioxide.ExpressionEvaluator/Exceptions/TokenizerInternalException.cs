using System;

namespace Dioxide.ExpressionEvaluator.Exceptions;

public class TokenizerInternalException : Exception
{
    public TokenizerInternalException(string message)
        : base(message) { }
}
