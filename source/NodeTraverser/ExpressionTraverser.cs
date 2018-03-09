using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual BaseAHKNode expression(BaseAHKNode context)
		{
			context = objectDispatcher(context);
			return visitor.expression(context);
		}
	}
}