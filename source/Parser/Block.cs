using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
		public BaseVisitor visitor;
		public string parse(string code)
		{
			if (visitor == null)
				visitor = new defaultVisitor();
			
			int i = 0;
			return functionDeclaration("qwe(\tvar\t,var2\t\n,\tvar=123    \n, 		\tvar *\t)\t\n\t{\nvar=123\nvar2=12345}", ref i)?.ToString();
		}

		/*
			Blocks which are allowed inside a function.
		 */
		object functionBodyBlock(string code, ref int origin)
		{
			return variableAssign(code, ref origin);
		}
	}

	class defaultVisitor : BaseVisitor {}
}