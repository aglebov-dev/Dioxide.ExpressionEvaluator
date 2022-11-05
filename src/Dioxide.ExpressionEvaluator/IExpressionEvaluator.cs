using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Evaluation;

namespace Dioxide.ExpressionEvaluator
{
    public interface IExpressionEvaluator
    {
        double Calculate(string expression);
        ExpressionEvaluator AddFunction(string name, CustomEvalFunction function);
        ExpressionEvaluator AddVariable(string name, double value);
        ExpressionEvaluator SetContext(IContext context);
        ExpressionEvaluator SetParser(IParser parser);
    }
}
