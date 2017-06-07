using System;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
		public string parse(string code)
		{
            int i = 0;
			return variableOrFunctionChaining("qwe.asd.qwefunc ( \"qwe\" \n, \" asd \" )", ref i);
		}
	}
}