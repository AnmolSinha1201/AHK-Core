using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		classDeclarationClass classDeclaration(classDeclarationClass context)
		{
			for (int i = 0; i < context.classBody.Count; i++)
				context.classBody[i] = objectDispatcher(context.classBody[i]);
				
			return visitor.classDeclaration(context);
		}
	}
}