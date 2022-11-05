using System;
using System.Collections.Generic;
using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Exceptions;

namespace Dioxide.ExpressionEvaluator.Evaluation;

internal sealed class Context : IContext
{
    private static StringComparer _comparer = StringComparer.OrdinalIgnoreCase;
    private readonly HashSet<string> _executingFunctions = new(_comparer);
    private readonly Dictionary<string, double> _variables = new(_comparer);
    private readonly Dictionary<string, CustomEvalFunction> _functions = new(_comparer);

    public double ResolveVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
        {
            throw new UnknownVariableException(
                $"Unknown variable: '{name}'. Check that it has been added to the calculation context.");
        }

        return value;
    }

    public IContext AddFunction(string name, CustomEvalFunction function)
    {
        _functions[name] = function;
        return this;
    }

    public IContext AddVariable(string name, double value)
    {
        _variables[name] = value;
        return this;
    }

    public double CallFunction(string name, double[] args)
        => _functions.GetValueOrDefault(name)?.Invoke(args)
            ?? throw new UnknownFunctionException($"Unknown function: '{name}'. Check that it has been added to the calculation context.");

    public void AddExecutingFunction(string name)
    {
        if (_executingFunctions.Contains(name))
        {
            throw new RecursiveFunctionCallException($"Recursive function call {name}");
        }

        _executingFunctions.Add(name);
    }

    public void RemoveExecutingFunction(string name)
        => _executingFunctions.Remove(name);
}
