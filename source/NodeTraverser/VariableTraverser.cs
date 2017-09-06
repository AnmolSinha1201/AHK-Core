using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		variableClass variable(variableClass context)
		{
			return visitor.variable(context);
		}

		complexVariableClass complexVariable(complexVariableClass context)
		{
			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = objectDispatcher(context.chain[i]);
			
			for (int i = 0; i < context.chain.Count; i++)
				context.variableChain[i] = objectDispatcher(context.variableChain[i]);

			return visitor.complexVariable(context);
		}

		variableAssignClass variableAssign(variableAssignClass context)
		{
			context.variable = complexVariable(context.variable);
			context.expression = objectDispatcher(context.expression);

			return visitor.variableAssign(context);
		}
	}
}