using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class binaryOperationLinkClass : IAHKNode
		{
			public string op;
			public IAHKNode expression;

			public binaryOperationLinkClass(string op, IAHKNode expression)
			{
				this.op = op;
				this.expression = expression;
			}

			public override string ToString() =>(op == null? "" : op + " ") + expression;

			public IAHKNode extraInfo {get; set;}
		}

		// the first item of the list should always be a head. A head's op = null
		public class binaryOperationClass : IAHKNode
		{
			public List<binaryOperationLinkClass> binaryOperationLinkList;

			public binaryOperationClass(List<binaryOperationLinkClass> binaryOpLinkList)
			{
				this.binaryOperationLinkList = binaryOpLinkList;
			}

			public override string ToString() => binaryOperationLinkList.Flatten(" ");

			public IAHKNode extraInfo {get; set;}
		}
	}
}