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

		variableDeclarationClass variableDeclaration(variableDeclarationClass context)
		{
			return visitor.variableDeclaration(context);
		}

		complexVariableClass complexVariable(complexVariableClass context)
		{
			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = visitor.expression(context.chain[i]);
			
			context.variable = visitor.expression(context.variable);

			return visitor.complexVariable(context);
		}

		variableAssignClass variableAssign(variableAssignClass context)
		{
			context.complexVariable = complexVariable(context.complexVariable);
			context.expression = expression(context.expression);

			return visitor.variableAssign(context);
		}
	}
}