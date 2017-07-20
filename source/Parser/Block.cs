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
			return functionDeclaration("qwe(\tvar\t,var2\t\n,\tvar=123    \n, 		\tvar *\t)\t\n\t{}", ref i)?.ToString();
		}

		/*
			Each block will have its own driver loop so it can better control which parts are chainable and which are not.
		 */

		/*
			Blocks which are allowed inside a function.
		 */
		List<object> functionBodyBlock(string code, ref int origin)
		{
			return new List<object>();
		}
	}

	class defaultVisitor : BaseVisitor {}
}