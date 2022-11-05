using System;
using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Exceptions;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeBinary : INode
{
    private readonly INode _left;
    private readonly INode _right;
    private readonly TokenType _tokenType;

    public NodeBinary(INode left, INode right, TokenType tokenType)
    {
        _left = left;
        _right = right;
        _tokenType = tokenType;
    }

    public double Eval(IContext context)
    {
        var left = _left.Eval(context);
        var right = _right.Eval(context);

        return _tokenType switch
        {
            TokenType.Add => left + right,
            TokenType.Subtract => left - right,
            TokenType.Multiply => left * right,
            TokenType.Divide => left / right,
            TokenType.Pow => Math.Pow(left, right),
            var tokenType => throw new EvaluationInternalException($"Unknow operation for evaluation expression '{tokenType}({left}, {right})'.")
        };
    }
}
