using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class dotUnwrapClass : IAHKNode
		{
			public IAHKNode variableOrFunction;

			public dotUnwrapClass(IAHKNode variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
			}

			public override string ToString() => "." + variableOrFunction;

			public IAHKNode extraInfo {get; set;}
		}

		public class bracketUnwrapClass : IExtraInfo
		{
			public IAHKNode expression;

			public bracketUnwrapClass(IAHKNode expression)
			{
				this.expression = expression;
			}

			public override string ToString() => "[" + expression + "]";

			public IAHKNode extraInfo {get; set;}
		}
	}
}