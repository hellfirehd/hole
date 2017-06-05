using System;

namespace Pdsi.Hole.Ast
{
	public class BinaryOperator : INode
	{
		readonly INode _left;
		readonly Token _op;
		readonly INode _right;

		public BinaryOperator(INode left, Token op, INode right)
		{
			_left = left;
			_op = op;
			_right = right;
		}

		public Token Op => _op;

		public INode Left => _left;
		public INode Right => _right;

		public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
	}
}
