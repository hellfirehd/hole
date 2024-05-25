namespace Pdsi.Hole.Ast;

public class UnaryOperator(Token op, INode expression) : INode
{
	public Token Op { get; } = op;

	public INode Expression { get; } = expression;

	public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
}
