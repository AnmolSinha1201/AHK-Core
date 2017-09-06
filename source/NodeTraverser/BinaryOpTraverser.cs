using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		binaryOperationClass binaryOperation(binaryOperationClass context)
		{
			for (int i = 0; i < context.binaryOperationLinkList.Count; i++)
				context.binaryOperationLinkList[i] = visitor.binaryOperationLink(context.binaryOperationLinkList[i]);
				
			return visitor.binaryOperation(context);
		}
	}
}