using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		expressionClass expression(expressionClass context)
		{
			context.value = objectDispatcher(context.value);
			return visitor.expression(context);
		}
	}
}