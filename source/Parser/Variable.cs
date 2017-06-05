using System;

namespace AHKCore
{
	partial class Parser
	{
		string variable(string code, ref int origin)
		{
			return NAME(code, ref origin);
		}
	}
}