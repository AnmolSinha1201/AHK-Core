using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class Parser
	{
		ternaryOperationClass ternaryOperation(string code, ref int origin, BaseAHKNode precursor)
		{
			int pos = origin;

			CRLFWS(code, ref pos);
			if (stringMatcher(code, ref pos, "?") == null)
				return null;
			
			WS(code, ref pos);
			var ifTrue = Expression(code, ref pos);
			if (ifTrue == null)
				return null;
			
			CRLFWS(code, ref pos);
			if (stringMatcher(code, ref pos, ":") == null)
				return null;
			
			WS(code, ref pos);
			var ifFalse = Expression(code, ref pos);
			if (ifFalse == null)
				return null;

			origin = pos;
			return visitor.ternaryOperation(new ternaryOperationClass(precursor, ifTrue, ifFalse));
		}
	}
}