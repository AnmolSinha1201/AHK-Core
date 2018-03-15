using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class directiveClass : BaseAHKNode
		{
			public string directiveName, directiveParam;

			public directiveClass(string directiveName, string directiveParam)
			{
				this.directiveName = directiveName;
				this.directiveParam = directiveParam;
			}

			public override string ToString() => $"#{directiveName} {directiveParam}";
		}
	}
}