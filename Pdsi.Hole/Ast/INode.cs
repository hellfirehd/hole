using System;

namespace Pdsi.Hole.Ast
{
	public interface INode
	{
		Int32 Accept(INodeVisitor visitor);
	}
}
