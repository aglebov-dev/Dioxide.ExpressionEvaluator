namespace Dioxide.ExpressionEvaluator.Tokens
{
    internal struct Token
    {
        public TokenType Type { get; }
        public decimal Value { get; }
        public string Identifier { get; }
        public bool IsSymbolToken => Type is
            TokenType.Add or
            TokenType.Subtract or
            TokenType.Multiply or
            TokenType.Divide or
            TokenType.OpenParens or
            TokenType.CloseParens or
            TokenType.Comma;

        private Token(TokenType type, decimal value, string identifier)
        {
            Type = type;
            Value = value;
            Identifier = identifier;
        }

        public Token(TokenType type)
            : this(type, default, string.Empty) { }

        public Token(decimal value)
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
                TokenType.Number => Value.ToString(),
                TokenType.Identifier => Identifier,
                TokenType.EOF => "EOF",
                _ => "[ERROR]"
            };
        }
    }
}
