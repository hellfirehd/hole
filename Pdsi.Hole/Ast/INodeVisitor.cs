namespace Pdsi.Hole.Ast;

public interface INodeVisitor
{
	Int32 Visit(Number node);
	Int32 Visit(UnaryOperator node);
	Int32 Visit(BinaryOperator node);
}
