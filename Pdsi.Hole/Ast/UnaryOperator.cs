using System;

namespace Pdsi.Hole.Ast
{
	public class UnaryOperator : INode
	{
		readonly Token _op;
		readonly INode _expression;

		public UnaryOperator(Token op, INode expression)
		{
			_op = op;
			_expression = expression;
		}

		public Token Op => _op;

		public INode Expression => _expression;

		public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
	}
}
