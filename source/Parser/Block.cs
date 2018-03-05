using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public partial class Parser
	{
		public BaseVisitor visitor;

		public List<IAHKNode> Test()
		{
			return parse("class qwe{var=123\nvar2=456}\nclass asd{var=123\nvar2=456}");
		}

		public List<IAHKNode> parse(string code)
		{
			if (visitor == null)
				visitor = new defaultVisitor();
			
			int i = 0;
			return chunk(code, ref i);
		}

		List<IAHKNode> chunk(string code, ref int origin)
		{
			var chunkList = new List<IAHKNode>();
			while (true)
			{
				CRLFWS(code, ref origin);
				var _block = block(code, ref origin);
				if (_block == null)
					break;
				chunkList.Add(_block);
				if (CRLF(code, ref origin) == null)
					break;
			}

			return chunkList;
		}

		IAHKNode block(string code, ref int origin)
		{
			return classDeclaration(code, ref origin) ?? functionDeclaration(code, ref origin) ?? 
			functionBodyBlock(code, ref origin);
		}

		/*
			Blocks which are allowed inside a function.
		 */
		IAHKNode functionBodyBlock(string code, ref int origin)
		{
			return variableAssign(code, ref origin);
		}

		IAHKNode loopBlock(string code, ref int origin)
		{
			return breakBlock(code, ref origin) ?? continueBLock(code, ref origin) ?? functionBodyBlock(code, ref origin);
		}

		List<IAHKNode> loopBodyBlock(string code, ref int origin)
		{
			return loopBodyBlockWithParentheses(code, ref origin) ?? loopBodyBlockWithoutParentheses(code, ref origin);
		}

		List<IAHKNode> loopBodyBlockWithParentheses(string code, ref int origin)
		{
			int pos = origin;
			var loopBlockList = new List<IAHKNode>();

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

		List<IAHKNode> loopBodyBlockWithoutParentheses(string code, ref int origin)
		{
			var loopBlockList = new List<IAHKNode>();

			var lBody = loopBlock(code, ref origin);
			if (lBody == null)
				return null;
			loopBlockList.Add(lBody);

			return loopBlockList;
		}

		IAHKNode conditionalBlock(string code, ref int origin)
		{
			return conditionalBlockWithParentheses(code, ref origin) ?? conditionalBlockWithoutParentheses(code, ref origin);
		}

		IAHKNode conditionalBlockWithParentheses(string code, ref int origin)
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

		IAHKNode conditionalBlockWithoutParentheses(string code, ref int origin)
		{
			return Expression(code, ref origin);
		}

		List<IAHKNode> classBlock(string code, ref int origin)
		{
			int pos = origin;
			var blockList = new List<IAHKNode>();

			if (stringMatcher(code, ref pos, "{") == null)
				return null;
			CRLFWS(code, ref pos);

			while (true)
			{
				var retVal = classDeclaration(code, ref pos) ?? functionDeclaration(code, ref pos) ?? (IAHKNode) variableAssign(code, ref pos);
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