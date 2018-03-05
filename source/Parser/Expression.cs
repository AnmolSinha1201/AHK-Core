using System;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		/*
		 */
		BaseAHKNode Expression(string code, ref int origin)
		{
			return STRING(code, ref origin) ?? NUMBER(code, ref origin) ?? complexVariable(code, ref origin);
		}
	}
}