using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public BaseVisitor visitor;

		public NodeTraverser()
		{
			if (visitor == null)
				visitor = new defaultVisitor();
		}

		public List<object> TraverseNodes(List<object> AHKNodes)
		{
			for (int i = 0; i < AHKNodes.Count; i++)
				AHKNodes[i] = objectDispatcher(AHKNodes[i]);

			return AHKNodes;
		}
	}
}