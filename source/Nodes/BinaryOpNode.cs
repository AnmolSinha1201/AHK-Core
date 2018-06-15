using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		// to be used with binary operations
		public class opClass : BaseAHKNode
		{
			public string op;

			public opClass(string op)
			{
				this.op = op;
			}

			public override string ToString() => op;
		}

		// the first item of the list should always be a head. A head's op = null
		public class binaryOperationClass : BaseAHKNode
		{
			public List<BaseAHKNode> binaryOperationList;

			public binaryOperationClass(List<BaseAHKNode> binaryOpLinkList)
			{
				this.binaryOperationList = binaryOpLinkList;
			}

			public override string ToString() => binaryOperationList.Flatten(" ");
		}
	}
}