using System;
using Dioxide.ExpressionEvaluator.Tokenizers;
using Dioxide.ExpressionEvaluator.Tokens;
using Xunit;

namespace Dioxide.ExpressionEvaluator.Tests
{

    public class TestOne
    {
        [Fact]
        public void T1()
        {
            var span = "AD = 1234.5491".ToCharArray();
            var offset = 5;

            var token = NumberTokenizer.Get(ref offset, ref span);

            Assert.Equal(14, offset);
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal(1234.5491, token.Value);
            Assert.Empty(token.Identifier);
        }

        [Fact]
        public void T2()
        {
            var span = "AD = 12.34.5491".ToCharArray();
            var offset = 5;

            var token = NumberTokenizer.Get(ref offset, ref span);

            Assert.Equal(10, offset);
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal(12.34, token.Value);
            Assert.Empty(token.Identifier);
        }

        [Fact]
        public void T3()
        {
            var span = "900 + AD -   OFP + 4.3".ToCharArray();
            var offset = 10;

            var token = IdentifierTokenizer.Get(ref offset, ref span);

            Assert.Equal(16, offset);
            Assert.Equal(TokenType.Identifier, token.Type);
            Assert.Equal("OFP", token.Identifier);
            Assert.Equal(0, token.Value);
        }

        [Fact]
        public void T4()
        {
            var span = "900 + AD -   OFP + 4.3".ToCharArray();

            var d = span.AsSpan().Slice(2, 0);
        }
    }
}
