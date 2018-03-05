using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class dotUnwrapClass : BaseAHKNode
		{
			public BaseAHKNode variableOrFunction;

			public dotUnwrapClass(BaseAHKNode variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
			}

			public override string ToString() => "." + variableOrFunction;
		}

		public class bracketUnwrapClass : BaseAHKNode
		{
			public BaseAHKNode expression;

			public bracketUnwrapClass(BaseAHKNode expression)
			{
				this.expression = expression;
			}

			public override string ToString() => "[" + expression + "]";
		}
	}
}