using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeFunctionCall : INode
{
    private readonly string _functionName;
    private readonly INode[] _arguments;

    public NodeFunctionCall(string functionName, INode[] arguments)
    {
        _functionName = functionName;
        _arguments = arguments;
    }

    public double Eval(IContext context)
    {
        context.AddExecutingFunction(_functionName);

        var argVals = new double[_arguments.Length];
        for (var i = 0; i < _arguments.Length; i++)
        {
            argVals[i] = _arguments[i].Eval(context);
        }

        context.RemoveExecutingFunction(_functionName);
        
        return context.CallFunction(_functionName, argVals);
    }
}
