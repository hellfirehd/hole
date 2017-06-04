using System;

namespace Pdsi.Hole
{
	public class Interpreter
	{
		private Lexer _lexer;
		private Token _currentToken;

		public Interpreter(Lexer lexer)
		{
			_lexer = lexer;
			_currentToken = _lexer.GetNextToken();
		}

		private void Eat(TokenType tokenType)
		{
			if (_currentToken.TokenType == tokenType) {
				_currentToken = _lexer.GetNextToken();
				return;
			}
			throw new InterpreterException($"Did not expect {_currentToken}.");
		}

		private Int32 Factor()
		{
			var token = _currentToken;

			if (token.TokenType == TokenType.Integer) {
				Eat(TokenType.Integer);
				return token.Value;
			}

			if (token.TokenType == TokenType.LeftParenthesis) {
				Eat(TokenType.LeftParenthesis);
				var result = Expression();
				Eat(TokenType.RightParenthesis);
				return result;
			}

			throw new InterpreterException($"Did not expect {_currentToken}.");
		}

		public Int32 Term()
		{
			var result = Factor();

			while (_currentToken.TokenType == TokenType.Multiply || _currentToken.TokenType == TokenType.Divide) {
				switch (_currentToken.TokenType) {
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

		public Int32 Expression()
		{
			var result = Term();

			while (_currentToken.TokenType == TokenType.Add || _currentToken.TokenType == TokenType.Subtract) {
				switch (_currentToken.TokenType) {
					case TokenType.Add:
						Eat(TokenType.Add);
						result = result + Term();
						break;
					case TokenType.Subtract:
						Eat(TokenType.Subtract);
						result = result - Term();
						break;
				}
			}

			return result;
		}

	}
}
