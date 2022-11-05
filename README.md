# Expression evaluator

Application for calculating mathematical expressions with prioritization of operations and support for variables and functions in formulas.



## Supported

#### Operations:

```
'+' - Add
'-' - Subtract
'*' - Multiply
'/' - Divide
'^' - Pow
```

#### Functions and variables:

You can add any function to evaluate the expression - see code examples



## Code samples

#### Simple math expression

```csharp
var expression = "33.0 - 88.0 * (12.0 + 3.0 - 10.0) / 100.0 + 88.0 + 10.0";

IExpressionEvaluator evaluator = new ExpressionEvaluator();

var value = evaluator.Calculate(expression); // 126.6
```



#### Expression with variables

```csharp
var expression = "var_a/var_b + 100";

IExpressionEvaluator evaluator = new ExpressionEvaluator()
     .AddVariable("var_a", 14)
     .AddVariable("var_b", 7);

var value = evaluator.Calculate(expression); // 102
```



#### Expression with functions

```csharp
var expression = "-2.7^8 + func_a( 9*33.5+func_b(80, 3, 17.07) )";

IExpressionEvaluator evaluator = new ExpressionEvaluator()
    .AddFunction("FUNC_A", args => args[0] + 100)
    .AddFunction("FUNC_B", args => args[0] + args[1] + args[2])
    .AddVariable("VAR_F", 88.09);

var value = evaluator.Calculate(expression); //3325.8653648100017
```

