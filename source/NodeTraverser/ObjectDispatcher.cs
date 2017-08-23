using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		object objectDispatcher(object o)
		{
			switch (o)
			{
				case variableAssignClass v :
					return visitor.variableAssign(v);

				case classDeclarationClass c :
					return visitor.classDeclaration(c);

				case functionDeclarationClass f :
					return visitor.functionDeclaration(f);

				case functionCallClass f :
					return visitor.functionCall(f);

				default :
					return null;
			}
		}
	}
}