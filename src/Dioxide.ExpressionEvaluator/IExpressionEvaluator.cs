using System;
using Dioxide.ExpressionEvaluator.Abstract;

namespace Dioxide.ExpressionEvaluator
{
    public interface IExpressionEvaluator
    {
        decimal Calculate(string expression);
        ExpressionEvaluator AddFunction(string name, Func<decimal[], decimal> function);
        ExpressionEvaluator AddVariable(string name, decimal value);
        ExpressionEvaluator SetContext(IContext context);
        ExpressionEvaluator SetParser(IParser parser);
    }
}
