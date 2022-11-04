namespace Dioxide.ExpressionEvaluator.Abstract;

public interface INode
{
    decimal Eval(IContext ctx);
}
