using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		// Op followed by var or var followed by op
		public class unaryOperationClass : BaseAHKNode
		{
			public List<BaseAHKNode> unaryOperationList;

			public unaryOperationClass(List<BaseAHKNode> unaryOpLinkList)
			{
				this.unaryOperationList = unaryOpLinkList;
			}

			public override string ToString() => unaryOperationList.Flatten("");
		}
	}
}