using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual ifElseBlockClass ifElseBlock(ifElseBlockClass context)
		{
			return context;
		}

		public virtual ifBlockClass ifBlock(ifBlockClass context)
		{
			return context;
		}

		public virtual elseBlockClass elseBlock(elseBlockClass context)
		{
			return context;
		}
	}
}