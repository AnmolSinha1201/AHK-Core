using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual variableClass variable(variableClass context)
		{
			return visitor.variable(context);
		}

		public virtual variableDeclarationClass variableDeclaration(variableDeclarationClass context)
		{
			return visitor.variableDeclaration(context);
		}

		public virtual complexVariableClass complexVariable(complexVariableClass context)
		{
			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = expression(context.chain[i]);
			
			context.variable = expression(context.variable);

			return visitor.complexVariable(context);
		}

		public virtual variableAssignClass variableAssign(variableAssignClass context)
		{
			context.complexVariable = complexVariable(context.complexVariable);
			context.expression = expression(context.expression);

			return visitor.variableAssign(context);
		}
	}
}