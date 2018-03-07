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
			// for (int i = 0; i < context.functionParameterList.Count; i++)
			// 	context.functionParameterList[i] = objectDispatcher(context.functionParameterList[i]);

			return visitor.functionCall(context);
		}

		/*
				- If last element is a functionCall, it will be executed by traverser.
				- If it is a bracketUnwrap, execute it manually using functionParameterList.
				- This is done to avoid using extraInfo in explicitly.
			*/
		complexFunctionCallClass complexFunctionCall(complexFunctionCallClass context)
		{
			for (int i = 0; i < context.functionParameterList.Count; i++)
				context.functionParameterList[i] = objectDispatcher(context.functionParameterList[i]);

			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = objectDispatcher(context.chain[i]);
			
			context.function = objectDispatcher(context.function);
			
			return visitor.complexFunctionCall(context);
		}
	}
}