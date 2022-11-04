using System;
using Dioxide.ExpressionEvaluator.Exceptions;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Tokenizers
{
    internal static class IdentifierTokenizer
    {
        public static Token Get(ref int offset, ref char[] array)
        {
            WhiteSpaceTokenizer.ForwardCursor(ref offset, ref array);
            if (offset >= array.Length)
            {
                return new Token(TokenType.EOF);
            }

            var lenght = array.Length;
            var head = offset;

            while (array[offset] is var ch && char.IsLetterOrDigit(ch) || ch is '_')
            {
                offset++;
                if (offset >= lenght) break;
            }

            if (offset == head)
            {
                throw new TokenizerInternalException("Some went wrong.");
            }

            var identifier = array.AsSpan().Slice(head, offset - head).ToString();

            return new Token(identifier);
        }
    }
}
