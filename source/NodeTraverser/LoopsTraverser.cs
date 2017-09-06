using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		breakBlockClass breakBlock()
		{
			return visitor.breakBlock();
		}

		continueBlockClass continueBLock()
		{
			return visitor.continueBlock();
		}

		loopLoopClass loopLoop(loopLoopClass context)
		{
			for (int i = 0; i < context.loopBody.Count; i++)
				context.loopBody[i] = objectDispatcher(context.loopBody[i]);
			
			return visitor.loopLoop(context);
		}

		whileLoopClass whileLoop(whileLoopClass context)
		{
			context.condition = objectDispatcher(context.condition);
			
			for (int i = 0; i < context.loopBody.Count; i++)
				context.loopBody[i] = objectDispatcher(context.loopBody[i]);

			return visitor.whileLoop(context);			
		}

		foreachLoopClass foreachLoop(foreachLoopClass context) //for is not a command
		{
			context.key = variable(context.key);
			context.value = variable(context.value);
			context.iterationObject = objectDispatcher(context.iterationObject);

			for (int i = 0; i < context.loopBody.Count; i++)
				context.loopBody[i] = objectDispatcher(context.loopBody[i]);
			
			return visitor.foreachLoop(context);
		}
	}
}