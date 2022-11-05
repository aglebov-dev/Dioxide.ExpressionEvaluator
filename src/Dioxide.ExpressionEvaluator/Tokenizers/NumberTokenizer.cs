using System;
using Dioxide.ExpressionEvaluator.Exceptions;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Tokenizers
{
    internal static class NumberTokenizer
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
            var pointUsed = false;
            while (array[offset] is var ch && char.IsDigit(ch) || !pointUsed && (pointUsed = ch is '.'))
            {
                offset++;
                if (offset >= lenght) break;
            }

            if (offset == head)
            {
                throw new TokenizerInternalException("Some went wrong.");
            }

            var valueSpan = array.AsSpan().Slice(head, offset - head);
            var value = double.Parse(valueSpan);

            return new Token(value);
        }
    }
}
