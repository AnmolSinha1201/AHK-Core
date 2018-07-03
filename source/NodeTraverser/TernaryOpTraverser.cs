using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual ternaryOperationClass ternaryOperation(ternaryOperationClass context)
		{
			objectDispatcher(context.condition);
				
			return visitor.ternaryOperation(context);
		}
	}
}