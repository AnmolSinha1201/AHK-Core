using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public partial class NodeTraverser
	{
		public BaseVisitor visitor;

		public NodeTraverser(BaseVisitor visitor = null)
		{
			this.visitor = visitor ?? new defaultVisitor();
		}

		public List<BaseAHKNode> TraverseNodes(List<BaseAHKNode> AHKNodes)
		{
			for (int i = 0; i < AHKNodes.Count; i++)
				AHKNodes[i] = objectDispatcher(AHKNodes[i]);

			return AHKNodes;
		}
	}
}