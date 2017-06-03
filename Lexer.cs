using System;

namespace Pdsi.Hole
{
    public class Lexer
    {
        private string _text;
        private int _position;
        public Int32 Position => _position;
        private char CurrentChar => _position < _text.Length ? _text[_position] : Char.MinValue;
        private char NextChar => _position + 1 < _text.Length ? _text[_position + 1] : Char.MinValue;

        public Lexer(String text)
        {
            // client string input, e.g. "3+5"
            _text = text;
            // self.pos is an index into self.text
            _position = 0;
            // current token instance
        }

        private void AdvancePosition()
        {
            _position++;
            while (Char.IsWhiteSpace(CurrentChar))
            {
                _position++;
            };
        }

        public Token GetNextToken()
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
                    return new Token(TokenType.Add);
                case "-":
                    AdvancePosition();
                    return new Token(TokenType.Subtract);
                case "*":
                    AdvancePosition();
                    return new Token(TokenType.Multiply);
                case "/":
                    AdvancePosition();
                    return new Token(TokenType.Divide);
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

    }
}