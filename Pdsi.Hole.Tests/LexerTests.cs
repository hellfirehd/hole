using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pdsi.Hole.Tests
{
	[TestClass]
	public class LexerTests
	{
		[TestMethod]
		public void Lexer_parses_integers_correctly()
		{
			var text = "1 23 456 7890";

			var lexer = new Lexer(text);

			lexer.GetNextToken().Value.Should().Be(1);
			lexer.GetNextToken().Value.Should().Be(23);
			lexer.GetNextToken().Value.Should().Be(456);
			lexer.GetNextToken().Value.Should().Be(7890);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.EOF);
		}

		[TestMethod]
		public void Lexer_parses_operators_correctly()
		{
			var text = "+ - * /";

			var lexer = new Lexer(text);

			lexer.GetNextToken().TokenType.Should().Be(TokenType.Add);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Subtract);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Multiply);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Divide);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.EOF);
		}

		[TestMethod]
		public void Lexer_skips_whitespace()
		{
			var text = " 1  +   23 - 456   ";

			var lexer = new Lexer(text);

			lexer.GetNextToken().Value.Should().Be(1);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Add);
			lexer.GetNextToken().Value.Should().Be(23);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Subtract);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.Integer);
			lexer.GetNextToken().TokenType.Should().Be(TokenType.EOF);
		}
	}
}
