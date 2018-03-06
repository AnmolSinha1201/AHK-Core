using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class expressionClass : BaseAHKNode
		{
			public BaseAHKNode value;

			public expressionClass(BaseAHKNode value)
			{
				this.value = value;
			}

			public override string ToString() => $"{value}";
		}
	}
}