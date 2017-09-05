using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		functionDeclarationClass functionDeclaration(functionDeclarationClass context)
		{
			context.functionHead = functionHead(context.functionHead);

			for (int i = 0; i < context.functionBody.Count; i++)
				context.functionBody[i] = objectDispatcher(context.functionBody[i]);
			
			return visitor.functionDeclaration(context);
		}

		functionHeadClass functionHead(functionHeadClass context)
		{
			for (int i = 0; i < context.functionParameters.Count; i++)
				context.functionParameters[i] = visitor.parameterInfo(context.functionParameters[i]);

			return visitor.functionHead(context);
		}
	}
}