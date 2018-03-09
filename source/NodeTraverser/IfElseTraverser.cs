using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual ifElseBlockClass ifElseBlock(ifElseBlockClass context)
		{
			context.ifBlock = ifBlock(context.ifBlock);
			context.elseBlock = elseBlock(context.elseBlock);

			return visitor.ifElseBlock(context);
		}

		public virtual ifBlockClass ifBlock(ifBlockClass context)
		{
			context.condition = objectDispatcher(context.condition);

			for (int i = 0; i < context.body.Count; i++)
				context.body[i] = objectDispatcher(context.body[i]);

			return visitor.ifBlock(context);
		}

		public virtual elseBlockClass elseBlock(elseBlockClass context)
		{
			for (int i = 0; i < context.body.Count; i++)
				context.body[i] = objectDispatcher(context.body[i]);

			return visitor.elseBlock(context);
		}
	}
}