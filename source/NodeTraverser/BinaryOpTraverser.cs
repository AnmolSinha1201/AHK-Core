using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual binaryOperationClass binaryOperation(binaryOperationClass context)
		{
			context.binaryOperationList.Where(i => !(i is opClass)).ToList()
				.ForEach(o => objectDispatcher(o));
				
			return visitor.binaryOperation(context);
		}
	}
}