using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeVariable : INode
{
    string _variableName;

    public NodeVariable(string variableName)
    {
        _variableName = variableName;
    }

    public decimal Eval(IContext context)
    {
        return context.ResolveVariable(_variableName);
    }
}
