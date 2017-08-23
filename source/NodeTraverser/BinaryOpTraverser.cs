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
			return visitor.binaryOperation(context);
		}
	}
}