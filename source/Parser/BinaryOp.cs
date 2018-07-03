using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		binaryOperationClass binaryOperation(string code, ref int origin, BaseAHKNode precursor)
		{
			var binaryOpList = new List<BaseAHKNode>();
			binaryOpList.Add(precursor);
			
			while(true)
			{
				var link = mathematicalOperation(code, ref origin) ?? concatOperation(code, ref origin)
					?? logicalOperation(code, ref origin) ?? bitwiseOperation(code, ref origin);
				
				if (link == null)
					break;
				binaryOpList.AddRange(link);
			}

			if (binaryOpList.Count == 1) // only percursor added
				return null;
			return visitor.binaryOperation(new binaryOperationClass(binaryOpList));
		}

		List<BaseAHKNode> mathematicalOperation(string code, ref int origin)
		{
			int pos = origin;
			string[] ops = {"+", "-", "*", "/", "//", "**"};
			string op;
			
			CRLFWS(code, ref pos);
			// making sure its not var=123\n--var
			if ((op = opChecker(code, ref pos, new string[] {"--"})) != null)
				return null;
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return BinaryExpressionAppender(new opClass(op), expression);
		}

		List<BaseAHKNode> concatOperation(string code, ref int origin)
		{
			int pos = origin;

			if (CRLFWS(code, ref pos) == null)
				return null;
			
			if (stringMatcher(code, ref pos, ".") == null)
				return null;

			if (WS(code, ref pos) == null)
				return null;
			
			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;
			
			origin = pos;
			return BinaryExpressionAppender(new opClass("."), expression);
		}

		List<BaseAHKNode> logicalOperation(string code, ref int origin)
		{
			int pos = origin;
			string[] ops = {"<", ">", "=", "<=", ">=", "==", "!=", "&&", "||"};
			string op;
			
			CRLFWS(code, ref pos);
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return BinaryExpressionAppender(new opClass(op), expression);
		}

		List<BaseAHKNode> bitwiseOperation(string code, ref int origin)
		{
			int pos = origin;
			string[] ops = {"<<", ">>", "&", "|"};
			string op;
			
			CRLFWS(code, ref pos);
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return BinaryExpressionAppender(new opClass(op), expression);
		}

		List<BaseAHKNode> BinaryExpressionAppender(opClass op, BaseAHKNode item)
		{
			var retList = new List<BaseAHKNode>() { op };
			if (item is binaryOperationClass o)
				retList.AddRange(o.binaryOperationList);
			else
				retList.Add(item);

			return retList;
		}
	}
}