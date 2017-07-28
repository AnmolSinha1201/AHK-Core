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
			return classDeclaration("class qwe{var=123\nvar2=456}", ref i)?.ToString();
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

			if (stringMatcher(code, ref pos, "{") == null)
				return null;

			CRLFWS(code, ref pos);
			while (true)
			{
				var lBody = loopBlock(code, ref pos);
				if (lBody == null)
					break;
				loopBlockList.Add(lBody);

				WS(code, ref pos);
				if (CRLF(code, ref pos) == null) //compulsory CRLF so that various blocks can not be chained together
					break;
				CRLFWS(code, ref pos);
			}
			CRLFWS(code, ref pos);

			if (stringMatcher(code, ref pos, "}") == null)
				return null;

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

			if (stringMatcher(code, ref pos, "(") == null)
				return null;

			WS(code, ref pos);
			var expression = Expression(code, ref pos);
			WS(code, ref pos);

			if (stringMatcher(code, ref pos, ")") == null)
				return null;

			origin = pos;
			return expression;
		}

		object conditionalBlockWithoutParentheses(string code, ref int origin)
		{
			return Expression(code, ref origin);
		}

		List<object> classBlock(string code, ref int origin)
		{
			int pos = origin;
			var blockList = new List<object>();

			if (stringMatcher(code, ref pos, "{") == null)
				return null;
			CRLFWS(code, ref pos);

			while (true)
			{
				var retVal = classDeclaration(code, ref pos) ?? functionDeclaration(code, ref pos) ?? (object) variableAssign(code, ref pos);
				if (retVal == null)
					break;
				blockList.Add(retVal);

				if (CRLF(code, ref pos) == null)
					break;
			}

			WS(code, ref pos);
			if (stringMatcher(code, ref pos, "}") == null)
				return null;
			
			origin = pos;
			return blockList;
		}
	}

	class defaultVisitor : BaseVisitor {}
}