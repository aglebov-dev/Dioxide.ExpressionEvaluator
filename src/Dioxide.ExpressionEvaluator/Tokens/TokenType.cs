namespace Dioxide.ExpressionEvaluator.Tokens;

internal enum TokenType
{
    Unknown,
    EOF,
    Add,
    Subtract,
    Multiply,
    Divide,
    OpenParens,
    CloseParens,
    Comma,
    Identifier,
    Number,
    Pow
}

