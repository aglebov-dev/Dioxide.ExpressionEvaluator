using Dioxide.ExpressionEvaluator.Evaluation;

namespace Dioxide.ExpressionEvaluator.Abstract;

public interface IContext : IFuncContexKeeper
{
    double ResolveVariable(string name);
    double CallFunction(string name, double[] arguments);
    IContext AddFunction(string name, CustomEvalFunction function);
    IContext AddVariable(string name, double value);
}
