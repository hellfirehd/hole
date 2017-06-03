using System;

namespace Dkw.Hole
{
    public class Interpreter
    {
        private string _text;
        private int _position;
        private Int32 Position => _position;
        private char CurrentChar => _position < _text.Length ? _text[_position] : Char.MinValue;
        private char NextChar => _position + 1 < _text.Length ? _text[_position + 1] : Char.MinValue;
        private Token _currentToken;

        public Interpreter(String text)
        {
            // client string input, e.g. "3+5"
            _text = text;
            // self.pos is an index into self.text
            _position = 0;
            // current token instance
            _currentToken = null;
        }

        private void AdvancePosition()
        {
            _position++;
            while (Char.IsWhiteSpace(CurrentChar))
            {
                _position++;
            };
        }
        private Boolean CanAdvancePosition()
        {
            return _position + 1 < _text.Length;
        }

        private Token GetNextToken()
        {
            if (CurrentChar == Char.MinValue)
            {
                return new Token(TokenType.EOF);
            }

            if (Char.IsWhiteSpace(CurrentChar))
            {
                AdvancePosition();
            }

            String chunk = null;

            while (true)
            {
                chunk += CurrentChar;
                // Peek at the next position to see if it's the same class as the current position
                if (GetClass(CurrentChar) != GetClass(NextChar))
                {
                    break;
                }
                AdvancePosition();
            }

            if (Int32.TryParse(chunk, out var i))
            {
                AdvancePosition();
                return new Token<Int32>(TokenType.Integer, i);
            }

            switch (chunk)
            {
                case "+":
                    AdvancePosition();
                    return new Token(TokenType.Plus);
                case "-":
                    AdvancePosition();
                    return new Token(TokenType.Minus);
            }

            throw new Exception($"Error parsing input at {Position}: {chunk}");
        }

        private CharClass GetClass(char c)
        {
            if (Char.IsWhiteSpace(c)) return CharClass.WhiteSpace;
            if (Char.IsLetter(c)) return CharClass.Letter;
            if (Char.IsNumber(c)) return CharClass.Number;
            if (Char.IsDigit(c)) return CharClass.Digit;
            if (Char.IsPunctuation(c)) return CharClass.Punctuation;
            if (Char.IsSymbol(c)) return CharClass.Symbol;
            if (Char.IsSeparator(c)) return CharClass.Separator;
            if (Char.IsControl(c)) return CharClass.Control;
            throw new Exception($"I have no idea how to classify '{c}' at {Position}");
        }

        private void Eat(params TokenType[] types)
        {
            // compare the current token type with the passed token
            // type and if they match then "eat" the current token
            // and assign the next token to the self.current_token,
            // otherwise raise an exception.
            foreach (var t in types)
            {
                if (_currentToken.Type == t)
                {
                    _currentToken = GetNextToken();
                    return;
                }
            }
            throw new Exception($"Error eating token at {Position}.");
        }

        public Int32 Expr()
        {
            _currentToken = GetNextToken();

            var left = _currentToken;
            Eat(TokenType.Integer);

            var op = _currentToken;
            Eat(TokenType.Plus, TokenType.Minus);

            var right = _currentToken;
            Eat(TokenType.Integer);

            switch (op.Type)
            {
                case TokenType.Plus:
                    return ((Token<Int32>)left).Value + ((Token<Int32>)right).Value;
                case TokenType.Minus:
                    return ((Token<Int32>)left).Value - ((Token<Int32>)right).Value;
            }

            throw new Exception($"Didn't know what to do with: {op}");
        }
    }
}