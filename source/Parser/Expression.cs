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
			- 	unaryOp -> complexVariable (as variable will be consumed in case of postOp)
		 */
		BaseAHKNode Expression(string code, ref int origin)
		{
			var part1 = parenthesesExpression(code, ref origin)
			?? STRING(code, ref origin) ?? NUMBER(code, ref origin)
			?? newObject(code, ref origin)
			?? unaryOperation(code, ref origin)
			?? complexFunctionCall(code, ref origin) ?? complexVariable(code, ref origin)
			?? (BaseAHKNode)null;

			if (part1 == null)
				return null;
			
			var part2 = binaryOperation(code, ref origin, part1) 
			?? ternaryOperation(code, ref origin, part1)
			?? (BaseAHKNode)null;
			
			if (part2 != null)
				return part2;

			return part1;
		}

		parenthesesExpressionClass parenthesesExpression(string code, ref int origin)
		{
			var pos = origin;

			if (code[pos] != '(')
				return null;
			pos++;

			WS(code, ref pos);
			var expression = Expression(code, ref pos);
			WS(code, ref pos);

			if (code[pos] != ')')
				return null;
			pos++;

			origin = pos;
			return visitor.parenthesesExpression(new parenthesesExpressionClass(expression));

		}
	}
}