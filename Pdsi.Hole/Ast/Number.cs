using System;

namespace Pdsi.Hole.Ast
{
	public class Number : INode
	{
		readonly Token _number;

		public Number(Token token)
		{
			_number = token;
		}

		public Int32 Value => _number.Value;

		public Int32 Accept(INodeVisitor visitor) => visitor.Visit(this);
	}
}
