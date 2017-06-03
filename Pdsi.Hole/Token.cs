using System;

namespace Pdsi.Hole
{
    public class Token
    {
        private readonly TokenType _type;
        public Token(TokenType type)
        {
            _type = type;
        }
        public TokenType Type => _type;

        public override String ToString()
        {
            return $"Token({Type})";
        }
    }

    public class Token<T> : Token
    {
        private readonly T _value;

        public Token(TokenType type, T value)
            : base(type)
        {
            _value = value;
        }

        public T Value => _value;

        public override String ToString()
        {
            return $"Token({Type}, {_value})";
        }
    }
}