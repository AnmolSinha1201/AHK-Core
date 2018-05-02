using System;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		/*
			Order of execution:
			-	complexFunctionCall -> complexVariable (variable can consume NAME. So either
				check dotUnwrap content or simply execute complexFunctionCall earlier)
			-	newObject -> complexVariable (as "new" would be considered as a variable)
		 */
		BaseAHKNode Expression(string code, ref int origin)
		{
			return STRING(code, ref origin) ?? NUMBER(code, ref origin)
			?? newObject(code, ref origin)
			?? complexFunctionCall(code, ref origin) ?? complexVariable(code, ref origin)
			?? (BaseAHKNode)null;
		}
	}
}