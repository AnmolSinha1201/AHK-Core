using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region classDeclaration
		public class classDeclarationClass : ISearchable
		{
			public string className;
			public List<object> classBody;

			public classDeclarationClass(string className, List<object> classBody)
			{
				this.className = className;
				this.classBody = classBody;
			}

			public override string ToString() => $"class {className}\n{{\n\t{classBody.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get {return classBody;}
			}
		}

		public virtual classDeclarationClass classDeclaration(string className, List<object> classBody)
		{
			return new classDeclarationClass(className, classBody);
		}
		#endregion
	}
}