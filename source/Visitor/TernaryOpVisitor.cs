using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual ternaryOperationClass ternaryOperation(ternaryOperationClass context)
		{
			return context;
		}
	}
}