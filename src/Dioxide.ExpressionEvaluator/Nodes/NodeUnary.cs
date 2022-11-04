using System;
using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeUnary : INode
{
    private readonly INode _node;
    private readonly Func<decimal, decimal> _operation;

    public NodeUnary(INode node, Func<decimal, decimal> operation)
    {
        _node = node;
        _operation = operation;
    }

    public decimal Eval(IContext context)
    {
        var value = _node.Eval(context);

        var result = _operation(value);
        return result;
    }
}
