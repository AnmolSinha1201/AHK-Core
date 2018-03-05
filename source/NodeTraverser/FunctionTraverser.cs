using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		functionCallClass functionCall(functionCallClass context)
		{
			for (int i = 0; i < context.functionParameterList.Count; i++)
				context.functionParameterList[i] = objectDispatcher(context.functionParameterList[i]);

			return visitor.functionCall(context);
		}

		complexFunctionCallClass complexFunctionCall(complexFunctionCallClass context)
		{
			for (int i = 0; i < context.functionParameterList.Count; i++)
				context.functionParameterList[i] = objectDispatcher(context.functionParameterList[i]);

			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = objectDispatcher(context.chain[i]);
			
			return visitor.complexFunctionCall(context);
		}
	}
}