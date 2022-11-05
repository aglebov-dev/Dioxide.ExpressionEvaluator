namespace Dioxide.ExpressionEvaluator.Abstract;

public interface INode
{
    double Eval(IContext ctx);
}
