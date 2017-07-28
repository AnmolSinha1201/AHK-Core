using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region classDeclaration
		public class classDeclarationClass
		{
			public string defaultValue, className;
			public List<object> classBody;

			public classDeclarationClass(string className, List<object> classBody)
			{
				this.className = className;
				this.classBody = classBody;
				this.defaultValue = $"class {className}\n{{\n\t{classBody.FlattenAsChain("\n\t")}\n}}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual classDeclarationClass classDeclaration(string className, List<object> classBody)
		{
			return new classDeclarationClass(className, classBody);
		}
		#endregion
	}
}