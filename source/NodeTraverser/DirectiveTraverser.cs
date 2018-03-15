using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual directiveClass directive(directiveClass context)
		{			
			return visitor.directive(context);
		}
	}
}