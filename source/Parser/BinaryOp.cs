using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		binaryOperationClass binaryOperation(string code, ref int origin, BaseAHKNode precursor)
		{
			List<binaryOperationLinkClass> binaryOpLinkList = new List<binaryOperationLinkClass>();
			binaryOpLinkList.Add(new binaryOperationLinkClass(null, precursor));
			binaryOperationLinkClass o;

			while ((o = mathematicalOperation(code, ref origin)) != null)
				binaryOpLinkList.Add(o);
			return visitor.binaryOperation(new binaryOperationClass(binaryOpLinkList));
		}

		binaryOperationLinkClass mathematicalOperation(string code, ref int origin)
		{
			int pos = origin;
			string[] ops = {"+", "-", "*", "/", "//", "**"};
			string op;
			
			CRLFWS(code, ref pos);
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return visitor.binaryOperationLink(new binaryOperationLinkClass(op, expression));
		}

		binaryOperationLinkClass concatOperation(string code, ref int origin)
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
			return visitor.binaryOperationLink(new binaryOperationLinkClass(".", expression));
		}

		binaryOperationLinkClass logicalOperation(string code, ref int origin)
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
			return visitor.binaryOperationLink(new binaryOperationLinkClass(op, expression));
		}

		binaryOperationLinkClass bitwiseOperation(string code, ref int origin)
		{
			int pos = origin;
			string[] ops = {"<<", ">>"};
			string op;
			
			CRLFWS(code, ref pos);
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return visitor.binaryOperationLink(new binaryOperationLinkClass(op, expression));
		}
	}
}