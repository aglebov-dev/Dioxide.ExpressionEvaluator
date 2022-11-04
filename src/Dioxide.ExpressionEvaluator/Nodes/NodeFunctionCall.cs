using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator.Nodes;

internal sealed class NodeFunctionCall : INode
{
    private string _functionName;
    private INode[] _arguments;

    public NodeFunctionCall(string functionName, INode[] arguments)
    {
        _functionName = functionName;
        _arguments = arguments;
    }

    public decimal Eval(IContext context)
    {
        context.AddExecutingFunction(_functionName);

        var argVals = new decimal[_arguments.Length];
        for (var i = 0; i < _arguments.Length; i++)
        {
            argVals[i] = _arguments[i].Eval(context);
        }

        context.RemoveExecutingFunction(_functionName);
        
        return context.CallFunction(_functionName, argVals);
    }
}
