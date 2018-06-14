using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class parenthesesExpressionClass : BaseAHKNode
		{
			public BaseAHKNode expression;
			
			public parenthesesExpressionClass(BaseAHKNode expression)
			{
				this.expression = expression;
			}

			public override string ToString() => $"({expression})";
		}
	}
}