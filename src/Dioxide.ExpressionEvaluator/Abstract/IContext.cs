using System;

namespace Dioxide.ExpressionEvaluator.Abstract;

public interface IContext: IFuncContexKeeper
{
    decimal ResolveVariable(string name);
    decimal CallFunction(string name, decimal[] arguments);
    IContext AddFunction(string name, Func<decimal[], decimal> function);
    IContext AddVariable(string name, decimal value);
}
