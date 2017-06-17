using System;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
        string mathematicalOperation(string code, ref int origin)
        {
            int pos = origin;
            string[] ops = {"+", "-", "*", "/", "//", "**"};
            string op;

            if ((op = opChecker(code, ref pos, ops)) == null)
                return null;
            WS(code, ref pos);

            var expression = Expression(code, ref pos);
            if (expression == null)
                return null;

            return $"{op} {expression}";
        }
	}
}