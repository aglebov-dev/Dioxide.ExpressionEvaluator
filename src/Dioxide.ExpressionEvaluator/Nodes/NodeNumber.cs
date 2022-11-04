using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeNumber : INode
{
    private readonly decimal _number;

    public NodeNumber(decimal number)
    {
        _number = number;
    }

    public decimal Eval(IContext ctx)
    {
        return _number;
    }
}
