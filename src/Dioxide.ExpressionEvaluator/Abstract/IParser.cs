namespace Dioxide.ExpressionEvaluator.Abstract;

public interface IParser
{
    INode Parse(string text);
}
