namespace Dioxide.ExpressionEvaluator.Abstract;

public interface IFuncContexKeeper
{
    void AddExecutingFunction(string name);
    void RemoveExecutingFunction(string name);
}
