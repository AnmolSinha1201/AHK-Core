using System;
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
			return functionDeclarationParamterList("var\t,var2\t\n,\tvar=123    \n, 		\tvar *", ref i)?.FlattenAsFunctionParam();
		}
	}

	class defaultVisitor : BaseVisitor {}
}