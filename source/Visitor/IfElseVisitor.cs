using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual ifElseBlockClass ifElseBlock(ifBlockClass ifBlock, elseBlockClass elseBlock)
		{
			return new ifElseBlockClass(ifBlock, elseBlock);
		}

		public virtual ifBlockClass ifBlock(object condition, List<object> body)
		{
			return new ifBlockClass(condition, body);
		}

		public virtual elseBlockClass elseBlock(List<object> body)
		{
			return new elseBlockClass(body);
		}
	}
}