using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
        dotUnwrapClass dotUnwrap(dotUnwrapClass context)
		{
            context.variableOrFunction = objectDispatcher(context.variableOrFunction);
			return visitor.dotUnwrap(context);
		}

		bracketUnwrapClass bracketUnwrap(bracketUnwrapClass context)
		{
            context.expression = objectDispatcher(context.expression);
			return visitor.bracketUnwrap(context);
		}
	}
}