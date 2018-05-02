using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual classDeclarationClass classDeclaration(classDeclarationClass context)
		{
			return context;
		}

		public virtual newObjectClass newObject(newObjectClass context)
		{
			return context;
		}
	}
}