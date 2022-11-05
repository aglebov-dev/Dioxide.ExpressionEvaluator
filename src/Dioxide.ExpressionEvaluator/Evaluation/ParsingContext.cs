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

        public ParsingContext(IEnumerable<Token> tokens) => _tokens = tokens.ToArray();

        public Token CurrentToken => _offset < _tokens.Length
            ? _tokens[_offset]
            : _eof;

        public Token NextToken => _offset + 1 < _tokens.Length
            ? _tokens[_offset + 1]
            : _eof;

        public void MoveNext() => Interlocked.Increment(ref _offset);
    }
}
