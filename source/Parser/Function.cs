using System;
using System.Text;
using System.Collections.Generic;

namespace AHKCore
{
	partial class Parser
	{
		/*
			functionCall : NAME WS* functionParameter ;
			functionParameter : '(' WS* (expression? | (expression (CRLFWS* ',' WS* expression)*)) ')' ;

			implemented as
			
			functionCall : NAME WS* functionParameter;
			functionParameter : '(' WS* functionParameterList ')' ;
			functionParameterList : expression? | (expression (CRLFWS* ',' WS* expression)*) ;

			no need of WS* before ')' as functionParameterList ensures the WS. See Grammar file for more details.
			functionParameter is abstracted so it can be reused in complexFunctions.
		 */
		string functionCall(string code, ref int origin)
		{
			int pos = origin;
			string functionName = NAME(code, ref pos);
			if (functionName == null)
				return null;

			WS(code, ref pos);
			string functionParams = functionParameter(code, ref pos);
			if (functionParams == null)
				return null;
			
			origin = pos;
			return functionName + functionParams;
		}

		string functionParameter(string code, ref int origin)
		{
			int pos = origin;
			
			if (code.Length < pos + "(".Length)
				return null;
			if (code[pos] != '(')
				return null;
			pos++;

			WS(code, ref pos);
			List<string> expressionList = functionParameterList(code, ref pos);

			if (code.Length < pos + ")".Length)
				return null;
			if (code[pos] != ')')
				return null;
			pos++;

			origin = pos;
			return "(" + expressionList?.Count + ")";
		}

		/*
			functionParameterList is separated so that it can use its visitor and return a list instead of string.
		 */
		List<string> functionParameterList(string code, ref int origin)
		{
			int pos = origin;
			List<string> expressionList = new List<string>();
			string s;
			
			if ((s = Expression(code, ref pos)) == null)
				return null;
			expressionList.Add(s);

			while (true)
			{
				CRLFWS(code, ref pos);

				if (code.Length < pos + ",".Length)
					break;
				if (code[pos] != ',')
					break;
				pos++;

				WS(code, ref pos);
				if ((s = Expression(code, ref pos)) == null)
					return null;
				expressionList.Add(s);
			}
			
			origin = pos;
			return expressionList;
		}
	}
}