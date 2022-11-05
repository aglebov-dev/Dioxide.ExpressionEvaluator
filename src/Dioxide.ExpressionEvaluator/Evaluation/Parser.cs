using System;
using System.Collections.Generic;
using System.Linq;
using Dioxide.ExpressionEvaluator.Abstract;
using Dioxide.ExpressionEvaluator.Exceptions;
using Dioxide.ExpressionEvaluator.Nodes;
using Dioxide.ExpressionEvaluator.Tokenizers;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Evaluation
{
    internal sealed class Parser : IParser
    {
        private readonly Tokenizer _tokenizer = new();

        public INode Parse(string text)
        {
            var tokens = _tokenizer.GetTokens(text).ToArray();
            var context = new ParsingContext(tokens);

            var node = AddOrSubtract(context);
            if (context.CurrentToken.Type is TokenType.EOF || context.NextToken.Type is not TokenType.EOF)
            {
                throw new SyntaxException($"Unexpected characters at end of expression '{context.CurrentToken}{context.NextToken}'");
            }

            return node;
        }

        private INode AddOrSubtract(ParsingContext context)
        {
            var left = MultiplyOrDivide(context);

            while (context.NextToken.Type is TokenType.Add or TokenType.Subtract)
            {
                context.MoveNext();
                var type = context.CurrentToken.Type;
                var right = MultiplyOrDivide(context);

                left = new NodeBinary(left, right, type);
            }

            return left;
        }

        private INode MultiplyOrDivide(ParsingContext context)
        {
            var left = Pow(context);

            while (context.NextToken.Type is TokenType.Multiply or TokenType.Divide)
            {
                context.MoveNext();
                var type = context.CurrentToken.Type;
                var right = Pow(context);

                left = new NodeBinary(left, right, type);
            }

            return left;
        }

        private INode Pow(ParsingContext context)
        {
            var left = ParseUnary(context);
            while (context.NextToken.Type is TokenType.Pow)
            {
                context.MoveNext();
                var type = context.CurrentToken.Type;
                var right = ParseUnary(context);

                left = new NodeBinary(left, right, type);
            }

            return left;
        }

        private INode ParseUnary(ParsingContext context)
        {
            context.MoveNext();
            while (context.CurrentToken.Type is TokenType.Add)
                context.MoveNext();

            return context.CurrentToken.Type is TokenType.Subtract
                ? new NodeBinary(new NodeNumber(0), ParseUnary(context), TokenType.Subtract)
                : NumberOrIdentifier(context);
        }

        private INode NumberOrIdentifier(ParsingContext context)
        {
            var token = context.CurrentToken;
            return token.Type switch
            {
                TokenType.Number => new NodeNumber(token.Value),
                TokenType.OpenParens => GetSubExpression(context),
                TokenType.Identifier => GetVariableOrFunction(context),
                _ => throw new SyntaxException($"Unexpect token: {token.Type}")
            };
        }

        private INode GetSubExpression(ParsingContext context)
        {
            var node = AddOrSubtract(context);

            if (context.NextToken.Type is not TokenType.CloseParens)
                throw new SyntaxException("Missing close parenthesis");

            context.MoveNext();

            return node;
        }

        private INode GetVariableOrFunction(ParsingContext context)
        {
            var name = context.CurrentToken.Identifier;
            if (context.NextToken.Type is not TokenType.OpenParens)
            {
                return new NodeVariable(name);
            }

            context.MoveNext();
            var token = context.CurrentToken;
            if (token.Type is TokenType.OpenParens && context.NextToken.Type is TokenType.CloseParens)
            {
                context.MoveNext();
                return new NodeFunctionCall(name, Array.Empty<INode>());
            }

            var arguments = new List<INode>();
            while (true)
            {
                arguments.Add(AddOrSubtract(context));

                context.MoveNext();
                if (context.CurrentToken.Type is not TokenType.Comma)
                {
                    break;
                }
            }

            if (context.CurrentToken.Type is TokenType.CloseParens)
            {
                return new NodeFunctionCall(name, arguments.ToArray());
            }

            throw new SyntaxException("Missing close parenthesis");
        }
    }
}
