using System;

namespace Dkw.Hole
{
    public class Interpreter
    {
        private Lexer _lexer;
        private Token _currentToken;

        public Interpreter(Lexer lexer){
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
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
                    _currentToken = _lexer.GetNextToken();
                    return;
                }
            }
            throw new Exception($"Did not expect {_currentToken} at {_lexer.Position}.");
        }

        private Int32 Factor()
        {
            var token = _currentToken as Token<Int32>;
            Eat(TokenType.Integer);
            return token.Value;
        }

        public Int32 Expr() {
            var result = Factor();

            while (_currentToken.Type == TokenType.Multiply || _currentToken.Type == TokenType.Divide)
            {
                switch (_currentToken.Type)
                {
                    case TokenType.Multiply:
                        Eat(TokenType.Multiply);
                        result = result * Factor();
                        break;
                    case TokenType.Divide:
                        Eat(TokenType.Divide);
                        result = result / Factor();
                        break;
                }
            }

            return result;
        }

        public Int32 ExprOld()
        {
            var result = Factor();

            while (_currentToken.Type == TokenType.Add || _currentToken.Type == TokenType.Subtract)
            {
                switch (_currentToken.Type)
                {
                    case TokenType.Add:
                        Eat(TokenType.Add);
                        result = result + Factor();
                        break;
                    case TokenType.Subtract:
                        Eat(TokenType.Subtract);
                        result = result - Factor();
                        break;
                }
            }

            return result;
        }
    }
}
