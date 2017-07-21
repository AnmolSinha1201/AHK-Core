using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	partial class Parser
	{
        binaryOperationClass binaryOperation(string code, ref int origin, object precursor)
        {
            List<binaryOpLink> binaryOpLinkList = new List<binaryOpLink>();
            binaryOpLinkList.Add(new binaryOpLink(null, precursor));
            binaryOpLink o;

            while ((o = mathematicalOperation(code, ref origin)) != null)
                binaryOpLinkList.Add(o);
            return visitor.binaryOperation(binaryOpLinkList);
        }

        binaryOpLink mathematicalOperation(string code, ref int origin)
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
            return new binaryOpLink(op, expression);
        }

        binaryOpLink concatOperation(string code, ref int origin)
        {
            int pos = origin;

            if (CRLFWS(code, ref pos) == null)
                return null;
            
            if (code.Length < pos + ".".Length) //end of string
				return null;
			if (code[pos] != '.') 
				return null;

            if (WS(code, ref pos) == null)
                return null;
            
            var expression = Expression(code, ref pos);
            if (expression == null)
                return null;
            
            origin = pos;
            return new binaryOpLink(".", expression);
        }

        binaryOpLink logicalOperation(string code, ref int origin)
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
            return new binaryOpLink(op, expression);
        }

        binaryOpLink bitwiseOperation(string code, ref int origin)
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
            return new binaryOpLink(op, expression);
        }
	}
}