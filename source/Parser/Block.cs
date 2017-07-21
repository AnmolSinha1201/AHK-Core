using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	partial class Parser
	{
		public BaseVisitor visitor;
		public string parse(string code)
		{
			if (visitor == null)
				visitor = new defaultVisitor();
			
			int i = 0;
			return functionDeclaration("qwe(\tvar\t,var2\t\n,\tvar=123    \n, 		\tvar *\t)\t\n\t{\nvar=123\nvar2=12345}", ref i)?.ToString();
		}

		/*
			Blocks which are allowed inside a function.
		 */
		object functionBodyBlock(string code, ref int origin)
		{
			return variableAssign(code, ref origin);
		}

		Object loopBlock(string code, ref int origin)
		{
			return breakBlock(code, ref origin) ?? continueBLock(code, ref origin) ?? (object)functionBodyBlock(code, ref origin);
		}

		List<object> loopBodyBlock(string code, ref int origin)
		{
			return loopBodyBlockWithParentheses(code, ref origin) ?? loopBodyBlockWithoutParentheses(code, ref origin);
		}

		List<object> loopBodyBlockWithParentheses(string code, ref int origin)
		{
			int pos = origin;
			var loopBlockList = new List<object>();

			if (code.Length < pos + "{".Length)
				return null;
			if (code[pos] != '{')
				return null;
			pos++;

			CRLFWS(code, ref pos);
			while (true)
			{
				var lBody = loopBlock(code, ref pos);
				if (lBody == null)
					break;
				loopBlockList.Add(lBody);

				if (CRLF(code, ref pos) == null) //compulsory CRLF so that various blocks can not be chained together
					break;
			}
			CRLFWS(code, ref pos);

			if (code.Length < pos + "}".Length)
				return null;
			if (code[pos] != '}')
				return null;
			pos++;

			origin = pos;
			return loopBlockList;
		}

		List<object> loopBodyBlockWithoutParentheses(string code, ref int origin)
		{
			var loopBlockList = new List<object>();

			var lBody = loopBlock(code, ref origin);
			if (lBody == null)
				return null;
			loopBlockList.Add(lBody);

			return loopBlockList;
		}

		object conditionalBlock(string code, ref int origin)
		{
			return conditionalBlockWithParentheses(code, ref origin) ?? conditionalBlockWithoutParentheses(code, ref origin);
		}

		object conditionalBlockWithParentheses(string code, ref int origin)
		{
			int pos = origin;

			if (code.Length < pos + "(".Length)
				return null;
			if (code[pos] != '(')
				return null;
			pos++;

			WS(code, ref pos);
			var expression = Expression(code, ref pos);
			WS(code, ref pos);

			if (code.Length < pos + ")".Length)
				return null;
			if (code[pos] != ')')
				return null;
			pos++;

			origin = pos;
			return expression;
		}

		object conditionalBlockWithoutParentheses(string code, ref int origin)
		{
			return Expression(code, ref origin);
		}
	}

	class defaultVisitor : BaseVisitor {}
}