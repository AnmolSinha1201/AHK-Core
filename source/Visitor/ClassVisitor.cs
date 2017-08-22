using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual classDeclarationClass classDeclaration(string className, List<object> classBody)
		{
			return new classDeclarationClass(className, classBody);
		}
	}
}