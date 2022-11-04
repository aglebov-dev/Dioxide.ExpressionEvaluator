using System;
using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeBinary : INode
{
    private readonly INode _left;
    private readonly INode _right;
    private readonly Func<decimal, decimal, decimal> _operation;

    public NodeBinary(INode left, INode right, Func<decimal, decimal, decimal> operation)
    {
        _left = left;
        _right = right;
        _operation = operation;
    }

    public decimal Eval(IContext context)
    {
        var leftValue = _left.Eval(context);
        var rightValue = _right.Eval(context);

        return _operation(leftValue, rightValue);
    }
}
