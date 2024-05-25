using Pdsi.Hole.Ast;

namespace Pdsi.Hole;

public class Interpreter(Parser parser) : INodeVisitor
{
	private readonly Parser _parser = parser;

	public Int32 Visit(Number node) => node.Value;

	public Int32 Visit(UnaryOperator node) => node.Op.TokenType switch
	{
		TokenType.Add => +node.Expression.Accept(this),
		TokenType.Subtract => -node.Expression.Accept(this),
		_ => throw new InterpreterException($"Did not expect {node.Op}."),
	};

	public Int32 Visit(BinaryOperator node) => node.Op.TokenType switch
	{
		TokenType.Add => node.Left.Accept(this) + node.Right.Accept(this),
		TokenType.Subtract => node.Left.Accept(this) - node.Right.Accept(this),
		TokenType.Multiply => node.Left.Accept(this) * node.Right.Accept(this),
		TokenType.Divide => node.Left.Accept(this) / node.Right.Accept(this),
		_ => throw new InterpreterException($"Did not expect {node.Op}."),
	};

	public Int32 Interpret()
	{
		var ast = _parser.Parse();
		return ast.Accept(this);
	}
}
