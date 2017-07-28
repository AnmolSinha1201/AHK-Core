using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.BaseVisitor;

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
			return visitor.classDeclaration(className, classBody);
		}
	}
}