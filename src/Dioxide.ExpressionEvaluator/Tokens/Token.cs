namespace Dioxide.ExpressionEvaluator.Tokens
{
    internal struct Token
    {
        public TokenType Type { get; }
        public double Value { get; }
        public string Identifier { get; }

        private Token(TokenType type, double value, string identifier)
        {
            Type = type;
            Value = value;
            Identifier = identifier;
        }

        public Token(TokenType type)
            : this(type, default, string.Empty) { }

        public Token(double value)
            : this(TokenType.Number, value, string.Empty) { }

        public Token(string identifier)
            : this(TokenType.Identifier, default, identifier) { }


        public override string ToString()
        {
            return Type switch
            {
                TokenType.Add => "+",
                TokenType.Subtract => "-",
                TokenType.Multiply => "*",
                TokenType.Divide => "/",
                TokenType.OpenParens => "(",
                TokenType.CloseParens => ")",
                TokenType.Comma => ",",
                TokenType.Pow => "^",
                TokenType.Number => Value.ToString(),
                TokenType.Identifier => Identifier,
                TokenType.EOF => "EOF",
                _ => "[ERROR]"
            };
        }
    }
}
