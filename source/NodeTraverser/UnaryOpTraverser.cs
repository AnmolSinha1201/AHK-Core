using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual unaryOperationClass unaryOperation(unaryOperationClass context)
		{
			context.unaryOperationList.ForEach(o => objectDispatcher(o));
				
			return visitor.unaryOperation(context);
		}
	}
}