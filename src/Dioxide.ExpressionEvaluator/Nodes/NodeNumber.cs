using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeNumber : INode
{
    private readonly double _number;

    public NodeNumber(double number) => _number = number;
    public double Eval(IContext context) => _number;
}
