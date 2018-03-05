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
	}
}