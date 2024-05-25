namespace Pdsi.Hole.Ast;

public class BinaryOperator(INode left, Token op, INode right) : INode
{
	public Token Op { get; } = op;

	public INode Left { get; } = left;
	public INode Right { get; } = right;

	public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
}
