using System;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
		public string parse(string code)
		{
            int i = 0;
			return NAME(code, ref i);
		}
	}
}