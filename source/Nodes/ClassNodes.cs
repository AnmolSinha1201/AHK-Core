using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class classDeclarationClass : ISearchable, IExtraInfo
		{
			public string className;
			public List<object> classBody;

			public classDeclarationClass(string className, List<object> classBody)
			{
				this.className = className;
				this.classBody = classBody;
			}

			public override string ToString() => $"class {className}\n{{\n\t{classBody.Flatten("\n").Indent<string>()}\n}}";

			public List<object> Searchables
			{
				get {return classBody;}
			}

			public object extraInfo {get; set;}
		}
	}
}