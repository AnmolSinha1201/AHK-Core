using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		classDeclarationClass classDeclaration(string code, ref int origin)
		{
			int pos = origin;
			
			if (stringMatcher(code, ref pos, "class") == null)
				return null;
			if (WS(code, ref pos) == null)
				return null;

			var className = NAME(code, ref pos);
			if (className == null)
				return null;
			
			CRLFWS(code, ref pos);
			var classBody = classBlock(code, ref pos);
			if (classBody == null)
				return null;
			
			origin = pos;
			return visitor.classDeclaration(new classDeclarationClass(className, classBody));
		}

		newObjectClass newObject(string code, ref int origin)
		{
			int pos = origin;
			
			if (stringMatcher(code, ref pos, "new") == null)
				return null;
			if (WS(code, ref pos) == null)
				return null;

			var className = complexFunctionCall(code, ref pos);
			if (className == null)
				return null;
			
			origin = pos;
			return visitor.newObject(new newObjectClass(className.chain, (functionCallClass)className.function));
		}
	}
}