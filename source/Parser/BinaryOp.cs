using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
        string binaryOperation(string code, ref int origin)
        {
            List<object> part2 = new List<object>();
            object o;
            while ((o = mathematicalOperation(code, ref origin)) != null)
                part2.Add(o);
            return part2.FlattenAsChain();
        }

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

            origin = pos;
            return $"{op} {expression}";
        }
	}
}