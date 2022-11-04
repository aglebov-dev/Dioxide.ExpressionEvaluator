using System.Collections.Generic;
using Dioxide.ExpressionEvaluator.Tokens;

namespace Dioxide.ExpressionEvaluator.Tokenizers
{
    internal class Tokenizer
    {
        public IEnumerable<Token> GetTokens(string text)
        {
            var offset = 0;
            var array = text.ToCharArray();

            while (true)
            {
                var token = CommonTokenizer.GetToken(ref offset, ref array);
                yield return token;

                if (token.Type is TokenType.EOF)
                {
                    break;
                }
            }
        }
    }
}
