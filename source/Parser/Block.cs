using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public partial class Parser
	{
		public BaseVisitor visitor;

		public List<BaseAHKNode> Test()
		{
			return parse("class qwe{var=123\nvar2=456}\nclass asd{var=123\nvar2=456}");
		}

		public List<BaseAHKNode> parse(string code)
		{
			if (visitor == null)
				visitor = new defaultVisitor();
			
			int i = 0;
			return chunk(code, ref i);
		}

		List<BaseAHKNode> chunk(string code, ref int origin)
		{
			var chunkList = new List<BaseAHKNode>();
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

		BaseAHKNode block(string code, ref int origin)
		{
			return classDeclaration(code, ref origin) ?? functionDeclaration(code, ref origin) ?? 
			functionBodyBlock(code, ref origin) ?? directive(code, ref origin);
		}

		/*
			Blocks which are allowed inside a function.
			ifElseBlock -> complexFunctionCall (if () is absorbed by func ())
			while loop -> complexFunctionCall (while () is absorbed by func ())
		 */
		BaseAHKNode functionBodyBlock(string code, ref int origin)
		{
			return ifElseBlock(code, ref origin)
			?? loops(code, ref origin)
			?? variableAssign(code, ref origin) 
			?? complexFunctionCall(code, ref origin)
			?? returnBlock(code, ref origin)
			?? unaryOperation(code, ref origin)
			?? (BaseAHKNode)null;
		}

		BaseAHKNode loopBlock(string code, ref int origin)
		{
			return breakBlock(code, ref origin) ?? continueBLock(code, ref origin) ?? functionBodyBlock(code, ref origin);
		}

		List<BaseAHKNode> loopBodyBlock(string code, ref int origin)
		{
			return loopBodyBlockWithParentheses(code, ref origin) ?? loopBodyBlockWithoutParentheses(code, ref origin);
		}

		List<BaseAHKNode> loopBodyBlockWithParentheses(string code, ref int origin)
		{
			int pos = origin;
			var loopBlockList = new List<BaseAHKNode>();

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

		List<BaseAHKNode> loopBodyBlockWithoutParentheses(string code, ref int origin)
		{
			var loopBlockList = new List<BaseAHKNode>();

			var lBody = loopBlock(code, ref origin);
			if (lBody == null)
				return null;
			loopBlockList.Add(lBody);

			return loopBlockList;
		}

		BaseAHKNode conditionalBlock(string code, ref int origin)
		{
			return conditionalBlockWithParentheses(code, ref origin) ?? conditionalBlockWithoutParentheses(code, ref origin);
		}

		BaseAHKNode conditionalBlockWithParentheses(string code, ref int origin)
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

		BaseAHKNode conditionalBlockWithoutParentheses(string code, ref int origin)
		{
			return Expression(code, ref origin);
		}

		List<BaseAHKNode> classBlock(string code, ref int origin)
		{
			int pos = origin;
			var blockList = new List<BaseAHKNode>();

			if (stringMatcher(code, ref pos, "{") == null)
				return null;
			CRLFWS(code, ref pos);

			while (true)
			{
				var retVal = classDeclaration(code, ref pos) ?? functionDeclaration(code, ref pos) ?? (BaseAHKNode) variableAssign(code, ref pos);
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

		returnBlockClass returnBlock(string code, ref int origin)
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, "return")== null)
				return null;

			WS(code, ref pos);
			stringMatcher(code, ref pos,  ",");
			WS(code, ref pos);
			var expression = Expression(code, ref pos);

			origin = pos;
			return visitor.returnBlock(new returnBlockClass(expression));
		}
	}

	class defaultVisitor : BaseVisitor {}
}