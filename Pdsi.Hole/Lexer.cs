using System.Diagnostics.CodeAnalysis;

namespace Pdsi.Hole;

public class Lexer
{
	private readonly String _text;
	private Int32 _position;
	private Char _currentChar;

	public Lexer(String text)
	{
		_text = text;
		_position = 0;
		_currentChar = _text[_position];
	}

	private void Advance()
	{
		_position++;
		if (_position > _text.Length - 1)
			_currentChar = Char.MinValue;
		else
			_currentChar = _text[_position];
	}

	private void SkipWhiteSpace()
	{
		while (_currentChar != Char.MinValue && Char.IsWhiteSpace(_currentChar))
		{
			Advance();
		}
	}

	[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "It is here for completeness and may be used in the future.")]
	private Char Peek()
	{
		var peekPos = _position + 1;
		if (peekPos > _text.Length - 1)
		{
			return Char.MinValue;
		}

		return _text[peekPos];
	}

	private Int32 Integer()
	{
		var chunk = "";
		var chunkStart = _position;
		while (_currentChar != Char.MinValue && Char.IsDigit(_currentChar))
		{
			chunk += _currentChar;
			Advance();
		}

		if (Int32.TryParse(chunk, out var i))
		{
			return i;
		}

		throw new LexerException($"Error parsing input at {chunkStart}: {chunk}");
	}

	public Token GetNextToken()
	{
		while (_currentChar != Char.MinValue)
		{
			if (Char.IsWhiteSpace(_currentChar))
			{
				SkipWhiteSpace();
				continue;
			}

			if (Char.IsDigit(_currentChar))
			{
				return new Token(_position, TokenType.Integer, Integer());
			}

			switch (_currentChar)
			{
				case '+':
					Advance();
					return new Token(_position, TokenType.Add);
				case '-':
					Advance();
					return new Token(_position, TokenType.Subtract);
				case '*':
					Advance();
					return new Token(_position, TokenType.Multiply);
				case '/':
					Advance();
					return new Token(_position, TokenType.Divide);
				case '(':
					Advance();
					return new Token(_position, TokenType.LeftParenthesis);
				case ')':
					Advance();
					return new Token(_position, TokenType.RightParenthesis);
			}

			throw new LexerException($"No idea what to do with '{_currentChar}' at position {_position}.");
		}

		return new Token(_position, TokenType.EOF);
	}
}
