using Pdsi.Hole.Ast;

namespace Pdsi.Hole;

public class Parser
{
	private readonly Lexer _lexer;
	private Token _currentToken;

	public Parser(Lexer lexer)
	{
		_lexer = lexer;
		_currentToken = _lexer.GetNextToken();
	}

	public INode Parse() => Expression();

	private INode Expression()
	{
		var node = Term();

		while (_currentToken.TokenType is TokenType.Add or TokenType.Subtract)
		{
			var token = _currentToken;
			switch (token.TokenType)
			{
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

	private INode Term()
	{
		var node = Factor();

		while (_currentToken.TokenType is TokenType.Multiply or TokenType.Divide)
		{
			var token = _currentToken;
			switch (token.TokenType)
			{
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

	private INode Factor()
	{
		var token = _currentToken;

		if (token.TokenType == TokenType.Add)
		{
			Eat(TokenType.Add);
			return new UnaryOperator(token, Factor());
		}

		if (token.TokenType == TokenType.Subtract)
		{
			Eat(TokenType.Subtract);
			return new UnaryOperator(token, Factor());
		}

		if (token.TokenType == TokenType.Integer)
		{
			Eat(TokenType.Integer);
			return new Number(token);
		}

		if (token.TokenType == TokenType.LeftParenthesis)
		{
			Eat(TokenType.LeftParenthesis);
			var result = Expression();
			Eat(TokenType.RightParenthesis);
			return result;
		}

		throw Error();
	}

	private void Eat(TokenType tokenType)
	{
		if (_currentToken.TokenType == tokenType)
		{
			_currentToken = _lexer.GetNextToken();
			return;
		}

		throw Error();
	}

	private ParserException Error() => new($"Did not expect {_currentToken}.");
}
