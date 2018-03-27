using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class Parser
	{
		directiveClass directive(string code, ref int origin)
		{
			int pos = origin;

			if (code[pos] != '#')
				return null;
			pos++;
			WS(code, ref pos);
			var directive = NAME(code, ref pos);
			if (directive == null)
				return null;

			WS(code, ref pos);
			if (code[pos] == ',')
				pos++;
			WS(code, ref pos);

			int start = pos;
			while(code[pos] != '\n' && code[pos] != '\r')
				pos++;

			var param = code.Substring(start, pos - start);
			if (string.IsNullOrEmpty(param))
				return null; //possible error point

			origin = pos;
			return visitor.directive(new directiveClass(directive, param));
		}
	}
}