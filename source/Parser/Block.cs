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
			return binaryOperation("+2+3//4", ref i, "1").ToString();
		}
	}

	class defaultVisitor : BaseVisitor {}
}