using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual commandBlockClass commandBlock(List<object> commandBlockList)
		{
			return new commandBlockClass(commandBlockList);
		}
	}
}