using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual commandBlockClass commandBlock(commandBlockClass context)
		{			
			for (int i = 1; i < context.commandBlockList.Count; i++)
				context.commandBlockList[i] = objectDispatcher(context.commandBlockList[i]);
				
			return visitor.commandBlock(context);
		}
	}
}