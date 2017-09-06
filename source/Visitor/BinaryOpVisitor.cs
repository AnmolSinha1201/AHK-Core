using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual binaryOperationClass binaryOperation(binaryOperationClass context)
		{
			return context;
		}

		public virtual binaryOperationLinkClass binaryOperationLink(binaryOperationLinkClass context)
		{
			return context;
		}
	}
}