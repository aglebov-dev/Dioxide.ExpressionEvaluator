using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeVariable : INode
{
    string _variableName;

    public NodeVariable(string variableName) => _variableName = variableName;
    public double Eval(IContext context) => context.ResolveVariable(_variableName);
}
