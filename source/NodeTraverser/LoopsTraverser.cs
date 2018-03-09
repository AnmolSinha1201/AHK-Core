using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual breakBlockClass breakBlock()
		{
			return visitor.breakBlock();
		}

		public virtual continueBlockClass continueBLock()
		{
			return visitor.continueBlock();
		}

		public virtual loopLoopClass loopLoop(loopLoopClass context)
		{
			for (int i = 0; i < context.loopBody.Count; i++)
				context.loopBody[i] = objectDispatcher(context.loopBody[i]);
			
			return visitor.loopLoop(context);
		}

		public virtual whileLoopClass whileLoop(whileLoopClass context)
		{
			context.condition = objectDispatcher(context.condition);
			
			for (int i = 0; i < context.loopBody.Count; i++)
				context.loopBody[i] = objectDispatcher(context.loopBody[i]);

			return visitor.whileLoop(context);			
		}

		public virtual foreachLoopClass foreachLoop(foreachLoopClass context) //for is not a command
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