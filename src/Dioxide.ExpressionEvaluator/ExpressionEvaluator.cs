using System;
using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Evaluation;

namespace Dioxide.ExpressionEvaluator
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        private IParser _parser;
        private IContext _context;

        public ExpressionEvaluator()
        {
            _parser = new Parser();
            _context = new Context();
        }

        public double Calculate(string expression)
        {
            return _parser.Parse(expression).Eval(_context);
        }

        public ExpressionEvaluator SetContext(IContext context)
        {
            _context = context;
            return this;
        }

        public ExpressionEvaluator SetParser(IParser parser)
        {
            _parser = parser;
            return this;
        }

        public ExpressionEvaluator AddFunction(string name, CustomEvalFunction function)
        {
            _context.AddFunction(name, function);
            return this;
        }

        public ExpressionEvaluator AddVariable(string name, double value)
        {
            _context.AddVariable(name, value);
            return this;
        }
    }
}
