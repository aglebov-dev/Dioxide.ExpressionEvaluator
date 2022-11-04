namespace Dioxide.ExpressionEvaluator.Tokenizers
{
    internal static class WhiteSpaceTokenizer
    {
        public static void ForwardCursor(ref int offset, ref char[] array)
        {
            var lenght = array.Length;
            while (offset < lenght && array[offset] is var ch && char.IsWhiteSpace(ch))
            {
                offset++;
            }
        }
    }
}
