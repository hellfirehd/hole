using System;

namespace Dkw.Hole
{
    public class Interpreter
    {
        private string _text;
        private int _pos;
        private Token _current_token;

        public Interpreter(String text)
        {
            // client string input, e.g. "3+5"
            _text = text;
            // self.pos is an index into self.text
            _pos = 0;
            // current token instance
            _current_token = null;
        }

        private Token GetNextToken()
        {
            if (_pos > _text.Length - 1)
            {
                return new Token(TokenType.EOF);
            }

            EatWhiteSpace();

            String chunk = null;

            while (true)
            {
                chunk += _text[_pos];
                // Peek at the next position to see if it's the same class as the current position
                if (_pos + 1 > _text.Length - 1 || GetClass(_text[_pos]) != GetClass(_text[_pos + 1]))
                {
                    break;
                }
                _pos++;
            }

            if (Int32.TryParse(chunk, out var i))
            {
                _pos++;
                return new Token<Int32>(TokenType.Integer, i);
            }

            switch (chunk)
            {
                case "+":
                    _pos++;
                    return new Token(TokenType.Plus);
                case "-":
                    _pos++;
                    return new Token(TokenType.Minus);
            }

            throw new Exception($"Error parsing input at {_pos}: {chunk}");
        }

        private void EatWhiteSpace()
        {
            while (true)
            {
                if (_pos > _text.Length - 1) return;
                if (GetClass(_text[_pos]) != CharClass.WhiteSpace) return;
                _pos++;
            }
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
            throw new Exception($"I have no idea how to classify '{c}' at {_pos}");
        }

        private void Eat(params TokenType[] types)
        {
            // compare the current token type with the passed token
            // type and if they match then "eat" the current token
            // and assign the next token to the self.current_token,
            // otherwise raise an exception.
            foreach (var t in types)
            {
                if (_current_token.Type == t)
                {
                    _current_token = GetNextToken();
                    return;
                }
            }
            throw new Exception($"Error eating token at {_pos}.");
        }

        public Int32 Expr()
        {
            _current_token = GetNextToken();

            var left = _current_token;
            Eat(TokenType.Integer);

            var op = _current_token;
            Eat(TokenType.Plus, TokenType.Minus);

            var right = _current_token;
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