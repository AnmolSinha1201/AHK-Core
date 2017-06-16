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
			return variableAssign("this.zxc.qwefunc [ \"zxc \" ] [\"qwe\"]:=\"zxc\"", ref i).ToString();
		}
	}

	class defaultVisitor : BaseVisitor {}
}