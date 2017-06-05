using Pdsi.Hole.Ast;
using System;

namespace Pdsi.Hole
{
	public class Parser
	{
		readonly Lexer _lexer;
		Token _currentToken;

		public Parser(Lexer lexer)
		{
			_lexer = lexer;
			_currentToken = _lexer.GetNextToken();
		}

		public INode Parse() => Expression();

		INode Expression()
		{
			var node = Term();

			while (_currentToken.TokenType == TokenType.Add || _currentToken.TokenType == TokenType.Subtract) {
				var token = _currentToken;
				switch (token.TokenType) {
					case TokenType.Add:
						Eat(TokenType.Add);
						break;
					case TokenType.Subtract:
						Eat(TokenType.Subtract);
						break;
				}

				node = new BinaryOperator(node, token, Term());
			}

			return node;
		}

		INode Term()
		{
			var node = Factor();

			while (_currentToken.TokenType == TokenType.Multiply || _currentToken.TokenType == TokenType.Divide) {
				var token = _currentToken;
				switch (token.TokenType) {
					case TokenType.Multiply:
						Eat(TokenType.Multiply);
						break;
					case TokenType.Divide:
						Eat(TokenType.Divide);
						break;
				}
				node = new BinaryOperator(node, token, Factor());
			}

			return node;
		}

		INode Factor()
		{
			var token = _currentToken;

			if (token.TokenType == TokenType.Integer) {
				Eat(TokenType.Integer);
				return new Number(token);
			}

			if (token.TokenType == TokenType.LeftParenthesis) {
				Eat(TokenType.LeftParenthesis);
				var result = Expression();
				Eat(TokenType.RightParenthesis);
				return result;
			}

			throw Error();
		}

		void Eat(TokenType tokenType)
		{
			if (_currentToken.TokenType == tokenType) {
				_currentToken = _lexer.GetNextToken();
				return;
			}
			throw Error();
		}

		Exception Error() => new ParserException($"Did not expect {_currentToken}.");
	}
}
