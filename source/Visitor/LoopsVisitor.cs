using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual breakBlockClass breakBlock()
		{
			return new breakBlockClass();
		}

		public virtual continueBlockClass continueBlock()
		{
			return new continueBlockClass();
		}

		public virtual loopLoopClass loopLoop(loopLoopClass context)
		{
			return context;
		}

		public virtual whileLoopClass whileLoop(whileLoopClass context)
		{
			return context;
		}

		public virtual foreachLoopClass foreachLoop(foreachLoopClass context)
		{
			return context;
		}
	}
}