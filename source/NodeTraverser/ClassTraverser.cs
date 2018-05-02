using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual classDeclarationClass classDeclaration(classDeclarationClass context)
		{
			for (int i = 0; i < context.classBody.Count; i++)
				context.classBody[i] = objectDispatcher(context.classBody[i]);
				
			return visitor.classDeclaration(context);
		}

		public virtual newObjectClass newObject(newObjectClass context)
		{
			switch (context.className)
			{
				case functionCallClass o:
					for (int i = 0; i < o.functionParameterList.Count; i++)
						o.functionParameterList[i] = objectDispatcher(o.functionParameterList[i]);
				break;

				case dotUnwrapClass o:
					for (int i = 0; i < ((functionCallClass)o.variableOrFunction).functionParameterList.Count; i++)
						((functionCallClass)o.variableOrFunction).functionParameterList[i] 
							= objectDispatcher(((functionCallClass)o.variableOrFunction).functionParameterList[i]);
				break;
			}
			
			for (int i = 0; i < context.chain.Count; i++)
				context.chain[i] = objectDispatcher(context.chain[i]);
			
			return visitor.newObject(context);
		}
	}
}