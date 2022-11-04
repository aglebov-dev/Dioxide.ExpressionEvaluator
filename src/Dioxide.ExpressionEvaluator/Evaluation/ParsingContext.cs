using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Evaluation
{
    internal sealed class ParsingContext
    {
        private int _offset = -1;
        private readonly Token[] _tokens;

        private static Token _eof = new Token(TokenType.EOF);
        private static Dictionary<TokenType, Func<decimal, decimal, decimal>> _operations = new()
        {
            [TokenType.Add] = (a, b) => a + b,
            [TokenType.Subtract] = (a, b) => a - b,
            [TokenType.Multiply] = (a, b) => a * b,
            [TokenType.Divide] = (a, b) => a / b
        };

        public ParsingContext(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToArray();
        }

        public Func<decimal, decimal, decimal> GetOperation(TokenType type)
            => _operations.GetValueOrDefault(type);

        public Token CurrentToken => _offset < _tokens.Length
            ? _tokens[_offset]
            : _eof;

        public Token NextToken => _offset + 1 < _tokens.Length
            ? _tokens[_offset + 1]
            : _eof;

        public void MoveNext() => Interlocked.Increment(ref _offset);
    }
}
