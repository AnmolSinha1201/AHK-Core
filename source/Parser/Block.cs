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
			return complexFunctionCall("this.qwefunc( \"123 \")", ref i).ToString();
		}
	}

	class defaultVisitor : BaseVisitor {}
}