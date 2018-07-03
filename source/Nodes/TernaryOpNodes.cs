using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		// Op followed by var or var followed by op
		public class ternaryOperationClass : BaseAHKNode
		{
			public BaseAHKNode condition, ifTrue, ifFalse;

			public ternaryOperationClass(BaseAHKNode condition, BaseAHKNode ifTrue, BaseAHKNode ifFalse)
			{
				this.condition = condition;
				this.ifTrue = ifTrue;
				this.ifFalse = ifFalse;
			}

			public override string ToString() => $"{condition} ? {ifTrue} : {ifFalse}";
		}
	}
}