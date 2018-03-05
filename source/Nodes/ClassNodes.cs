using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class classDeclarationClass : IAHKNode
		{
			public string className;
			public List<IAHKNode> classBody;

			public classDeclarationClass(string className, List<IAHKNode> classBody)
			{
				this.className = className;
				this.classBody = classBody;
			}

			public override string ToString() => $"class {className}\n{{\n\t{classBody.Flatten("\n").Indent<string>()}\n}}";

			public IAHKNode extraInfo {get; set;}
		}
	}
}