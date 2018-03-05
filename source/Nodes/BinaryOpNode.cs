using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class binaryOperationLinkClass : BaseAHKNode
		{
			public string op;
			public BaseAHKNode expression;

			public binaryOperationLinkClass(string op, BaseAHKNode expression)
			{
				this.op = op;
				this.expression = expression;
			}

			public override string ToString() =>(op == null? "" : op + " ") + expression;
		}

		// the first item of the list should always be a head. A head's op = null
		public class binaryOperationClass : BaseAHKNode
		{
			public List<binaryOperationLinkClass> binaryOperationLinkList;

			public binaryOperationClass(List<binaryOperationLinkClass> binaryOpLinkList)
			{
				this.binaryOperationLinkList = binaryOpLinkList;
			}

			public override string ToString() => binaryOperationLinkList.Flatten(" ");
		}
	}
}