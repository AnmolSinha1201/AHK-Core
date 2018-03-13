using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual functionDeclarationClass functionDeclaration(functionDeclarationClass context)
		{
			context.functionHead = functionHead(context.functionHead);

			for (int i = 0; i < context.functionBody.Count; i++)
				context.functionBody[i] = objectDispatcher(context.functionBody[i]);
			
			return visitor.functionDeclaration(context);
		}

		public virtual functionHeadClass functionHead(functionHeadClass context)
		{
			for (int i = 0; i < context.functionParameters.Count; i++)
				context.functionParameters[i] = visitor.parameterInfo(context.functionParameters[i]);

			return visitor.functionHead(context);
		}

		public virtual returnBlockClass returnBlock(returnBlockClass context)
		{
			if (context.expression != null)
				context.expression = expression(context.expression);
			
			return visitor.returnBlock(context);
		}
	}
}