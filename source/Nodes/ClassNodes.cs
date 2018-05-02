using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class classDeclarationClass : BaseAHKNode
		{
			public string className;
			public List<BaseAHKNode> classBody;

			public classDeclarationClass(string className, List<BaseAHKNode> classBody)
			{
				this.className = className;
				this.classBody = classBody;
			}

			public override string ToString() => $"class {className}\n{{\n\t{classBody.Flatten("\n").Indent<string>()}\n}}";
		}

		public class newObjectClass : BaseAHKNode
		{
			public BaseAHKNode className;
			public List<BaseAHKNode> chain;

			public newObjectClass(List<BaseAHKNode> chain, BaseAHKNode className)
			{
				this.className = className;
				this.chain = chain;
			}

			public override string ToString() => $"new {chain.Flatten()}{(chain.Count == 0? "" : ".")}{className}";
		}
	}
}