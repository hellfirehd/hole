using Pdsi.Hole.Ast;
using System;

namespace Pdsi.Hole
{
	public class Interpreter : INodeVisitor
	{
		readonly Parser _lexer;

		public Interpreter(Parser parser)
		{
			_lexer = parser;
		}

		public Int32 Visit(Number node) => node.Value;

		public Int32 Visit(UnaryOperator node)
		{
			switch (node.Op.TokenType) {
				case TokenType.Add:
					return +node.Expression.Accept(this);
				case TokenType.Subtract:
					return -node.Expression.Accept(this);
			}

			throw new InterpreterException($"Did not expect {node.Op}.");
		}

		public Int32 Visit(BinaryOperator node)
		{
			switch (node.Op.TokenType) {
				case TokenType.Add:
					return node.Left.Accept(this) + node.Right.Accept(this);
				case TokenType.Subtract:
					return node.Left.Accept(this) - node.Right.Accept(this);
				case TokenType.Multiply:
					return node.Left.Accept(this) * node.Right.Accept(this);
				case TokenType.Divide:
					return node.Left.Accept(this) / node.Right.Accept(this);
			}

			throw new InterpreterException($"Did not expect {node.Op}.");
		}

		public Int32 Interpret()
		{
			var ast = _lexer.Parse();
			return ast.Accept(this);
		}
	}
}
