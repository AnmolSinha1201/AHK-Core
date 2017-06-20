using System;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
		object Expression(string code, ref int origin)
		{
			return STRING(code, ref origin) ?? NUMBER(code, ref origin);
		}
	}
}