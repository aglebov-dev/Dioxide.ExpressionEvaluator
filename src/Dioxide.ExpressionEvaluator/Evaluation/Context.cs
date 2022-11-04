using System;
using System.Collections.Generic;
using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Exceptions;

namespace Dioxide.ExpressionEvaluator.Evaluation;

internal sealed class Context : IContext
{
    private readonly HashSet<string> _executingFunctions;
    private readonly Dictionary<string, decimal> _variables;
    private readonly Dictionary<string, Func<decimal[], decimal>> _functions;

    public Context()
    {
        var comparer = StringComparer.OrdinalIgnoreCase;

        _executingFunctions = new HashSet<string>(comparer);
        _variables = new Dictionary<string, decimal>(comparer);
        _functions = new Dictionary<string, Func<decimal[], decimal>>(comparer);
    }

    public decimal ResolveVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
        {
            throw new UnknownVariableException($"Unknown variable: '{name}'. Check that it has been added to the calculation context.");
        }

        return value;
    }

    public IContext AddFunction(string name, Func<decimal[], decimal> function)
    {
        _functions[name] = function;
        return this;
    }

    public IContext AddVariable(string name, decimal value)
    {
        _variables[name] = value;
        return this;
    }

    public decimal CallFunction(string name, decimal[] arguments)
    {
        if (!_functions.TryGetValue(name, out var function))
        {
            throw new UnknownFunctionException($"Unknown function: '{name}'. Check that it has been added to the calculation context.");
        }

        return function(arguments);
    }

    public void AddExecutingFunction(string name)
    {
        if (_executingFunctions.Contains(name))
        {
            throw new RecursiveFunctionCallException($"Recursive function call {name}");
        }

        _executingFunctions.Add(name);
    }

    public void RemoveExecutingFunction(string name)
    {
        _executingFunctions.Remove(name);
    }
}
