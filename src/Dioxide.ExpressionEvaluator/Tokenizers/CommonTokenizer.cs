using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Tokenizers
{
    internal static class CommonTokenizer
    {
        public static Token GetToken(ref int offset, ref char[] array)
        {
            WhiteSpaceTokenizer.ForwardCursor(ref offset, ref array);
            if (offset >= array.Length)
            {
                return new Token(TokenType.EOF);
            }

            var ch = array[offset];

            var token = ch switch
            {
                '+' => new Token(TokenType.Add),
                '-' => new Token(TokenType.Subtract),
                '*' => new Token(TokenType.Multiply),
                '/' => new Token(TokenType.Divide),
                '(' => new Token(TokenType.OpenParens),
                ')' => new Token(TokenType.CloseParens),
                ',' => new Token(TokenType.Comma),
                _ when IsDigit(ref ch) => NumberTokenizer.Get(ref offset, ref array),
                _ when IsIdent(ref ch) => IdentifierTokenizer.Get(ref offset, ref array),
                _ => new Token(TokenType.Unknown)
            };

            if (token.IsSymbolToken) offset++;

            bool IsDigit(ref char ch) => char.IsDigit(ch) || ch == '.';
            bool IsIdent(ref char ch) => char.IsLetter(ch) || ch == '_';

            return token;
        }
    }
}
