namespace Pdsi.Hole.Ast;

public class Number(Token token) : INode
{
	private readonly Token _number = token;

	public Int32 Value => _number?.Value ?? 0;

	public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
}
