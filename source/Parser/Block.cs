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
			return complexVariable("this.qwefunc()", ref i);
		}
	}

	class defaultVisitor : BaseVisitor {}
}